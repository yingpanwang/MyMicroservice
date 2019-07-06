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
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> QueryAll();
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="order">排序表达式</param>
        /// <returns>符合条件的实体集合</returns>
        IEnumerable<TEntity> QueryAll<TOrder>(Expression<Func<TEntity, TOrder>> order);

    }
}