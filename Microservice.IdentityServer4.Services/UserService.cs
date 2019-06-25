using Microservice.IdentityServer4.DTO;
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

        //private readonly IBaseRepository<User> repository;

        //public UserService(IBaseRepository<User> repository)
        //{
        //    this.repository = repository;
        //}

        public bool AddUser(UserDTO user)
        {
            return Repository.Add(null);
        }

        public IEnumerable<UserDTO> QueryAllUser()
        {
            return Repository.QueryAll().Select(x => new UserDTO()
            {
                ID = x.ID,
                Name = x.Name,
                Password = x.Password
            });
        }
    }
}