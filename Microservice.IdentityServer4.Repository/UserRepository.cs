using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.Entities;
using Microservice.IdentityServer4.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISTDbContext dbContext) : base(dbContext)
        {
        }
    }
}