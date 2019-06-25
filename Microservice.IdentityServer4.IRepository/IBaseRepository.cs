using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.IRepository
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
        /// <returns></returns>
        bool Add(TEntity entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">要添加的实体集合</param>
        /// <returns></returns>
        bool AddRange(params TEntity[] entities);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> QueryAll();
    }
}