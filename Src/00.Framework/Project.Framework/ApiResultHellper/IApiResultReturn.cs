using Microsoft.AspNetCore.Mvc;
using Project.Framework.Commands;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.ApiResultHellper
{
    public interface IApiResultReturn
    {
        IActionResult SetCommand(CommandResult commandResult);
         IActionResult SetQuery(dynamic data);
        ApiResult SetResult(bool IsSuccess, StatusCode statusCode, string message, List<string> errors);
    }
}
