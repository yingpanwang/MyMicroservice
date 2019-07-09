﻿using Microservice.IdentityServer4.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Microservice.IdentityServer4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = userService.QueryAllUser();
            return Json(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            var users = userService.QueryAllUser().Where(x => x.ID == id);
            userService.DeleteUser(new DTO.UserDTO() { ID = "1" });
            return Json(users);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteByID(string id)
        {
            bool isDelete = userService.DeleteUser(new DTO.UserDTO() { ID = id });
            if (isDelete)
            {
                return OK(null);
            }
            else
            {
                return Error("删除失败");
            }
        }
    }
}