using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.Services
{
    /// <summary>
    /// 基础服务类
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public class BaseService<TRepository> where TRepository : class
    {
        /// <summary>
        /// 基础服务仓储依赖
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(TRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// 仓储
        /// </summary>
        protected TRepository Repository { get; set; }
    }
}