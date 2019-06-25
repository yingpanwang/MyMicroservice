using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.DTO
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}