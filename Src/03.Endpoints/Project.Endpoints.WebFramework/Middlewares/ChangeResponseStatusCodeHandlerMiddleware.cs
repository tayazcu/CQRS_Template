using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Project.Framework.Extensions;
using Project.Framework;
using System.Text;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace Project.Endpoints.WebFramework.Middlewares
{
    public static class ChangeResponseStatusCodeHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder ChangeResponseStatusHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ChangeResponseStatusCodeHandlerMiddleware>();
        }
    }

    public class ChangeResponseStatusCodeHandlerMiddleware
    {
        private const string statusCode = "statusCode";
        private readonly RequestDelegate _next;
        public ChangeResponseStatusCodeHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var originBody = httpContext.Response.Body;
            try
            {
                var memStream = new MemoryStream();
                httpContext.Response.Body = memStream;

                await _next(httpContext).ConfigureAwait(false);

                memStream.Position = 0;
                var responseBody = new StreamReader(memStream).ReadToEnd();

                #region Custom logic to modify response
                if (!IsSwagger(httpContext))
                {
                    string getItemValue = responseBody.GetItem("statusCode");
                    if (getItemValue.HasValue())
                    {
                        var oldStr = $"\"{statusCode}\":\"{getItemValue}\"";
                        string enumValue = (getItemValue.ToEnum<StatusCode>()).ToInt<StatusCode>().ToString();
                        var newStr = $"\"{statusCode}\":\"{enumValue}\"";
                        //newResponseBody = responseBody.ChangeItem(itemToChange, enumValue.ToString());
                        responseBody = responseBody.Replace(oldStr, newStr, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
                #endregion

                var memoryStreamModified = new MemoryStream();
                var sw = new StreamWriter(memoryStreamModified);
                sw.Write(responseBody);
                sw.Flush();
                memoryStreamModified.Position = 0;

                await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
            }
            finally
            {
                httpContext.Response.Body = originBody;
            }
        }
        private bool IsSwagger(HttpContext context)
        {
            StringValues isSwagger = context.Request.Headers.Values.FirstOrDefault(x => x[0].Contains("/swagger/index.html") || x.Contains("/swagger/v1/swagger.json"));
            if (isSwagger.Any())
                return true;
            return false;
        }
    }
}
