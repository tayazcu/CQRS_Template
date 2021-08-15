using Microsoft.AspNetCore.Mvc;
using Project.Framework.Commands;
using System.Linq;
using Project.Framework.Web;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;
using System.Collections;
using System.Collections.Generic;
using Project.Framework.Extensions;

namespace Project.Framework.ApiResultHellper
{
    public class ApiResultReturn : Controller, IApiResultReturn, IScopedDependency
    {
        public IActionResult SetCommand(CommandResult commandResult)
        {
            ApiResult result = new ApiResult(commandResult.IsSuccess, commandResult.StatusCode, commandResult.Message, commandResult.Errors.ToList());

            switch (commandResult.StatusCode)
            {
                case Framework.StatusCode.Success: return StatusCode(200, result);
                case Framework.StatusCode.ServerError: return StatusCode(500, result);
                case Framework.StatusCode.BadRequest: return StatusCode(400, result);
                case Framework.StatusCode.NotFound: return StatusCode(404, result);
                case Framework.StatusCode.ListEmpty: return StatusCode(400, result);
                case Framework.StatusCode.LogicError: return StatusCode(500, result);
                case Framework.StatusCode.UnAuthorized: return StatusCode(401, result);
                default: return StatusCode(500, result);
            }
        }
        public IActionResult SetQuery(dynamic data)
        {
            ApiResult<dynamic> result = new ApiResult<dynamic>(true, Framework.StatusCode.Success, data);
            return Ok(result);
        }
        public ApiResult SetResult(bool IsSuccess, StatusCode statusCode, string message, List<string> errors)
        {
            ApiResult result = new ApiResult(IsSuccess, statusCode, message, errors);
            return result;
        }
    }
}
