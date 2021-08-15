using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Endpoints.WebFramework.Middlewares
{
    public static class ResponseTimeHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeHandlerMiddleware>();
        }
    }
    public class  ResponseTimeHandlerMiddleware
    {
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
        private readonly RequestDelegate _next;
        public ResponseTimeHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() => {
                watch.Stop();
                long responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });
            return this._next(context);
        }
    }
}
