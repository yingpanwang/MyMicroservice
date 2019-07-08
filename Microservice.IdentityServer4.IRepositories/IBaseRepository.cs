using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Microservice.IdentityServer4.IRepositories
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>是否添加成功</returns>
        bool Add(TEntity entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">要添加的实体集合</param>
        /// <returns>是否批量添加成功</returns>
        bool AddRange(params TEntity[] entities);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="isLogicDelete">是否为逻辑删除(默认值:true)</param>
        /// <returns></returns>
        bool Delete(TEntity entity, bool isLogicDelete = true);

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> QueryAll();

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns>符合条件的实体集合</returns>
        IEnumerable<TEntity> QueryAll<TSort>(bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TSort">排序</typeparam>
        /// <param name="queryExpression">条件查询表达式</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        IEnumerable<TEntity> Query<TSort>(Expression<Func<TEntity,bool>> queryExpression, bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TSort">排序</typeparam>
        /// <param name="pageIndex">页码（默认值：1）</param>
        /// <param name="pageSize">数据量(默认值:20)</param>
        /// <param name="queryExpression">条件查询表达式</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        IEnumerable<TEntity> QueryPage<TSort>(int pageIndex = 1, int pageSize = 20, 
            Expression<Func<TEntity, bool>> queryExpression = null, bool isAsc = true, Expression<Func<TEntity, TSort>> sortExpression = null);
    }
}