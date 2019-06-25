using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.IdentityServer4.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public ISTDbContext Db { get; set; }

        public BaseRepository(ISTDbContext dbContext)
        {
            this.Db = dbContext;
        }

        public bool Add(TEntity entity)
        {
            return true;
        }

        public bool AddRange(params TEntity[] entities)
        {
            return true;
        }

        public IEnumerable<TEntity> QueryAll()
        {
            return Db.Set<TEntity>().ToList();
        }
    }
}