using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.ApiResultHellper;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.Endpoints.WebFramework.Middlewares
{
    public static class CustomAuthenticationHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthenticationHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthenticationHandlerMiddleware>();
        }
    }
    public class CustomAuthenticationHandlerMiddleware
    {
        private  RequestDelegate _next;
        private readonly IApiResultReturn _apiResultReturn;
        private readonly IResourceManager _resourceManager;

        public CustomAuthenticationHandlerMiddleware(RequestDelegate next, IApiResultReturn apiResultReturn, IResourceManager resourceManager)
        {
            _next = next;
            _apiResultReturn = apiResultReturn;
            _resourceManager = resourceManager;
        }
        public async Task Invoke(HttpContext context)
        {
            //string authHeader = context.Request.Headers["Authorization"];
            var path = context.Request.Path;
            if (path.HasValue && path.Value.ToLower().StartsWith("/admin"))
            {
                var hasAuthorization = context.Request.Headers.ContainsKey("Authorization");
                if (hasAuthorization)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await WriteToResponseAsync(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
        private async Task WriteToResponseAsync(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await _next(context);
            }
            else
            {
                ApiResult result = _apiResultReturn.SetResult(false, StatusCode.UnAuthorized, _resourceManager.GetName(SharedResource.Unauthorized), null);
                string json = JsonConvert.SerializeObject(result);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            
        }
        private bool IsSwagger(HttpContext context)
        {
            StringValues isSwagger = context.Request.Headers.Values.FirstOrDefault(x => x.Equals("/swagger/index.html") || x.Equals("/swagger/v1/swagger.json"));
            if (isSwagger.Any())
                return true;
            return false;
        }
    }
}
