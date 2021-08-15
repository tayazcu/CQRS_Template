using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Project.Framework.Web
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class BaseController : Controller
    {
        protected readonly CommandDispatcher _commandDispatcher;
        protected readonly QueryDispatcher _queryDispatcher;
        protected readonly IResourceManager _resourceManager;
        protected readonly IApiResultReturn _apiResultReturn;
        public BaseController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager , IApiResultReturn apiResultReturn)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _resourceManager = resourceManager;
            _apiResultReturn = apiResultReturn;
        }
    }
}
