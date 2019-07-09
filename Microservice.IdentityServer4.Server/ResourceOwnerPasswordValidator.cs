using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microservice.IdentityServer4.IServices;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microservice.IdentityServer4.Server
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userService;
        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userService.CheckAsync(context.UserName, context.Password);
            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            if (context.UserName == "admin" && context.Password == "123")
            {
                context.Result = new GrantValidationResult(
                subject: context.UserName,
                authenticationMethod: "custom",
                claims: GetUserClaims());
            }
            else if (user != null)
            {
                context.Result = new GrantValidationResult(
                subject: context.UserName,
                authenticationMethod: "custom",
                claims: GetUserClaims());
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
        }

        //可以根据需要设置相应的Claim
        private Claim[] GetUserClaims()
        {
            List<object> roles = new List<object>()
            {
                new { RoleId = "1", RoleName = "超级管理员1" },
                new { RoleId = "2", RoleName = "超级管理员2" },
                new { RoleId = "3", RoleName = "超级管理员3" },
                new { RoleId = "4", RoleName = "超级管理员4" }
            };
            object org = new { OrgCode = "1", OrgName = "组织机构" };

            return new Claim[]
            {
                new Claim("UserId", 1.ToString()),
                new Claim(JwtClaimTypes.Name,"wjk"),
                new Claim(JwtClaimTypes.GivenName, "jaycewu"),
                new Claim(JwtClaimTypes.FamilyName, "yyy"),
                new Claim(JwtClaimTypes.Email, "977865769@qq.com"),
                new Claim(JwtClaimTypes.Role,JArray.FromObject(roles).ToString()),
                new Claim("org",JObject.FromObject(org).ToString())
            };
        }
    }
}