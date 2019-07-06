using Microsoft.AspNetCore.Mvc;
using Microservice.IdentityServer4.IServices;
using Microsoft.Extensions.Logging;
using System;

namespace Microservice.IdentityServer4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public string Get()
        {
            var users = userService.QueryAllUser();
            bool addUser = userService.AddUser(new DTO.UserDTO() { ID = Guid.NewGuid().ToString(), Name = "测试用户名称", Password = "测试密码" });
            if (addUser)
            {
                return "添加成功";
            }
            return "this is account get method!";
        }

    }
}