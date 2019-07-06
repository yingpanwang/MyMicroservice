using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.IRepositories;
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
        protected readonly ILogger logger;

        /// <summary>
        /// 数据上下文
        /// </summary>
        protected ISTDbContext Db { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        /// <param name="logger">日志</param>
        protected BaseRepository(ISTDbContext dbContext,ILogger<TEntity> logger)
        {
            this.logger = logger;
            Db = dbContext;
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
                logger.LogInformation($" {this.GetType().Name} 添加信息");
                Db.Set<TEntity>().Add(entity);
                return Db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
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
                logger.LogInformation($"{this.GetType().Name} 批量添加信息: ");
                Db.Set<TEntity>().AddRange(entities);
                return Db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex,$"{this.GetType().Name} 错误信息: {ex.Message}");
                return false;
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
                    return Db.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="order">排序表达式</param>
        /// <returns>符合条件的实体集合</returns>
        public IEnumerable<TEntity> QueryAll<TOrder>(Expression<Func<TEntity, TOrder>> order)
        {
            try
            {
                if (order != null)
                {
                    return Db.Set<TEntity>().OrderByDescending(order).ToList();
                }
                else
                {
                    return Db.Set<TEntity>().ToList();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return null;
            }
        }
    }
}