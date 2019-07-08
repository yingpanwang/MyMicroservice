using Microservice.IdentityServer4.DTO;
using Microservice.IdentityServer4.Entities;
using Microservice.IdentityServer4.IRepositories;
using Microservice.IdentityServer4.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.IdentityServer4.Services
{
    public class UserService : BaseService<IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public bool AddUser(UserDTO user)
        {
            var entity = new User()
            {
                ID = user.ID,
                Name = user.Name,
                Password = user.Password
            };
            return Repository.Add(entity);
        }

        public bool DeleteUser(UserDTO user)
        {
            var entity = new User()
            {
                ID = user.ID,
                Name = user.Name,
                Password = user.Password
            };
            return Repository.Delete(entity);
        }

        public IEnumerable<UserDTO> QueryAllUser()
        {
            var result = Repository.QueryAll().Select(x => new UserDTO()
            {
                ID = x.ID,
                Name = x.Name,
                Password = x.Password
            });
            return result;
        }
    }
}