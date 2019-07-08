using Microservice.IdentityServer4.Server.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.IdentityServer4.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        protected JsonResult Json(object obj)
        {
            return new JsonResult(obj);
        }
        protected JsonResult OK(object obj)
        {
           var response =  new ResponseModel<object>(1,obj,null);
           return new JsonResult(response);
        }
        protected JsonResult OK<TData>(TData data) where TData:class
        {
            var response = new ResponseModel<TData>(1, data, null);
            return new JsonResult(response);
        }
        protected JsonResult Error(string message,object data = null)
        {
            var response = new ResponseModel<object>(0, data, message);
            return new JsonResult(response);
        }

    }
}
