using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.IdentityServer4.Server.Model.Response
{
    public class ResponseModel<TData> where TData:class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
        public ResponseModel(int code, TData data = null,string message = null)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }
}
