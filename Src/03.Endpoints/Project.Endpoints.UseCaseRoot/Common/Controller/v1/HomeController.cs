using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Queries;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Endpoints.UseCaseRoot.Common.Controller.v1
{
    [ApiVersion("1")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> LoggerAsp(CancellationToken cancellationToken)
        {
            _logger.LogInformation("---- log info ----");
            return Ok("ok");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> LoggerEmahRiseError(CancellationToken cancellationToken)
        {
            HttpContext.RiseError(new Exception("---- log RiseError elmah ----"));
            return Ok("ok");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SetError(CancellationToken cancellationToken)
        {
            object[] o = new object[2];
            o[0] = "a";
            o[1] = "a";
            o[2] = "a";
            o[3] = "a";
            return Ok("ok");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SetException(CancellationToken cancellationToken)
        {
            throw new Exception("aaaa");
            return Ok("ok");
        }
    }
}
