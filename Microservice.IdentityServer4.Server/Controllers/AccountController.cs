using Microsoft.AspNetCore.Mvc;
using Microservice.IdentityServer4.IServices;

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
            return "this is account get method!";
        }
    }
}