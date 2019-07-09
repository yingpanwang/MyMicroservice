using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Microservice.IdentityServer4.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// 数据上下文
        /// </summary>
        private readonly ISTDbContext _db;

        /// <summary>
        /// 实体表
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        /// <param name="logger">日志</param>
        protected BaseRepository(ISTDbContext dbContext, ILogger<TEntity> logger)
        {
            _logger = logger;
            _db = dbContext;
            _dbSet = _db.Set<TEntity>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>是否添加成功</returns>
        public bool Add(TEntity entity)
        {
            try
            {
                _logger.LogInformation($" {this.GetType().Name} 添加信息");
                _db.Set<TEntity>().Add(entity);
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">批量添加的实体集合</param>
        /// <returns>是否添加成功</returns>
        public bool AddRange(params TEntity[] entities)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name} 批量添加信息: ");
                _db.Set<TEntity>().AddRange(entities);
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{this.GetType().Name} 错误信息: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="isLogicDelete">是否为逻辑删除(默认值:true)</param>
        /// <returns></returns>
        public bool Delete(TEntity entity, bool isLogicDelete = true)
        {
            if (isLogicDelete)
            {
                var property = entity.GetType().GetProperty("IsDelete");
                if (property == null)
                {
                    throw new InvalidOperationException("无法逻辑删除该实体，请检查该实体是否具有逻辑删除属性");
                }
                _dbSet.Attach(entity).State = EntityState.Deleted;

                return _db.SaveChanges() > 0;
            }
            else
            {
                _logger.LogInformation($"{this.GetType().Name} 删除信息");
                _dbSet.Attach(entity).State = EntityState.Deleted;

                return _db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>符合条件的实体集合</returns>
        public IEnumerable<TEntity> QueryAll()
        {
            try
            {
                return _db.Set<TEntity>().AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns>符合条件的实体集合</returns>
        public IEnumerable<TEntity> QueryAll<TSort>(bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null)
        {
            try
            {
                if (sortExpression != null)
                {
                    return _db.Set<TEntity>().OrderByDescending(sortExpression).AsNoTracking().ToList();
                }
                else
                {
                    return _db.Set<TEntity>().AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TSort">排序</typeparam>
        /// <param name="queryExpression">条件查询表达式</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query<TSort>(Expression<Func<TEntity, bool>> queryExpression, bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null)
        {
            var queryResult = _db.Set<TEntity>().Where(queryExpression);
            if (isAsc)
            {
                queryResult = queryResult.OrderBy(sortExpression);
            }
            else
            {
                queryResult = queryResult.OrderByDescending(sortExpression);
            }

            return queryResult.AsNoTracking();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TSort">排序</typeparam>
        /// <param name="pageIndex">页码（默认值：1）</param>
        /// <param name="pageSize">数据量(默认值:20)</param>
        /// <param name="queryExpression">条件查询表达式</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> QueryPage<TSort>(int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, bool>> queryExpression = null, bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null)
        {
            var queryResult = _db.Set<TEntity>().Where(queryExpression);
            if (isAsc)
            {
                queryResult = queryResult.OrderBy(sortExpression);
            }
            else
            {
                queryResult = queryResult.OrderByDescending(sortExpression);
            }

            var pageResult = queryResult.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return pageResult;
        }
    }
}