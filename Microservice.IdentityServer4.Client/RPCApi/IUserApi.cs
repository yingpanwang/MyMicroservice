using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace Microservice.IdentityServer4.Client.RPCApi
{
    public interface IUserApi : IHttpApi
    {
        [WebApiClient.Attributes.HttpGet("api/account")]
        Task<string> Get();
    }
}