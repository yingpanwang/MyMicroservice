using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.ApiMiddlewares
{
    /// <summary>
    /// 记录接口响应时间中间件
    /// </summary>
    public class ResponseTimeMiddleware
    {
        // 自定义响应头信息
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";

        public readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
#warning 接口响应时间中间件添加对外扩展的Builder模式-----记录至日志中,过滤功能,或者其他管理模块,
            if (path.StartsWithSegments("/api"))
            {
               // 开始记录执行时间使用Stopwatch
                var watch = new Stopwatch();
                watch.Start();
                context.Response.OnStarting(() => {
                    // 停止计时并记录执行时间保存至自定义响应头信息中
                    watch.Stop();
                    var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                    context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                    return Task.CompletedTask;
                });
            }
            return this._next(context);
        }
    }
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseResponseCollection(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseTimeMiddleware>();
            return app;
        }
    }
}
