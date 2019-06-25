using Microservice.IdentityServer4.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}