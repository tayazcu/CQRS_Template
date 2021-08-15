using Microsoft.AspNetCore.Mvc;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System.Security.Claims;

namespace Project.Framework.Web
{
    [Area("Admin")]
    [Route("[area]/api/v{version:apiVersion}/[controller]")]
    public class BaseAdminController : Controller
    {
        protected readonly CommandDispatcher _commandDispatcher;
        protected readonly QueryDispatcher _queryDispatcher;
        protected readonly IResourceManager _resourceManager;
        protected readonly IApiResultReturn _apiResultReturn;
        public BaseAdminController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IApiResultReturn apiResultReturn)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _resourceManager = resourceManager;
            _apiResultReturn = apiResultReturn;
        }

        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
        public string GetUserName => UserIsAutheticated ? HttpContext.User.Identity.Name : string.Empty;
        public string GetUserID => UserIsAutheticated ? HttpContext.User.Identity.GetUserId() : string.Empty;
        public bool IsInRole(string role) => HttpContext.User.IsInRole(role) ? true : false;
        public bool IsInClaim(string type, string claim) => ((((ClaimsIdentity)User.Identity).HasClaim(type, claim))) ? true : false;


    }
}
