using Microservice.IdentityServer4.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.IServices
{
    public interface IUserService
    {
        bool AddUser(UserDTO user);

        IEnumerable<UserDTO> QueryAllUser();
    }
}