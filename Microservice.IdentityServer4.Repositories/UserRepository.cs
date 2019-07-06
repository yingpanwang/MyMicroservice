using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.Entities;
using Microservice.IdentityServer4.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.IdentityServer4.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISTDbContext dbContext,ILogger<User> logger) : base(dbContext,logger)
        {
        }
    }
}