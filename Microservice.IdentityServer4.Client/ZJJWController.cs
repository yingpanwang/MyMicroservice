using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microservice.IdentityServer4.Client
{
    public class ZJJWController : ControllerBase
    {
        public AuthInfo MyAuthInfo
        {
            get
            {
                var user = HttpContext.User;
                var authInfo = new AuthInfo()
                {
                    Name = user.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value,
                    Org = JsonConvert.DeserializeObject<Org>(user.Claims.FirstOrDefault(x => x.Type == "org")?.Value),
                    Roles = JsonConvert.DeserializeObject<List<Role>>(user.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Role)?.Value)
                };
                return authInfo;
            }
        }
    }

    public class AuthInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("org")]
        public Org Org { get; set; }

        [JsonProperty("role")]
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Org
    {
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
    }
}