using Microservice.IdentityServer4.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.IdentityServer4.IServices
{
    public interface IUserService
    {
        Task<UserDTO> CheckAsync(string account, string password);

        bool AddUser(UserDTO user);

        bool DeleteUser(UserDTO user);

        IEnumerable<UserDTO> QueryAllUser();
    }
}