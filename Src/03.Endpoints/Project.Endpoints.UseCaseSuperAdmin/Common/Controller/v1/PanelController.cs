using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Users.Dtos;
using Project.Core.Domain.Users.Queries;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Queries;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Endpoints.UseCaseSuperAdmin.Common.Controller.v1
{
    [ApiVersion("1")]
    public class PanelController : BaseController
    {
        private readonly IMapper _mapper;
        public PanelController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager , IMapper mapper, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public IActionResult Login(LoginVM model)
        {
            AccessToken token = _queryDispatcher.Dispatch<AccessToken>(_mapper.Map<LoginVM, LoginQuery>(model));
            return _apiResultReturn.SetQuery(token);
        }

        /// <summary>
        /// for login with swagger
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("[action]")]
        public virtual ActionResult Token([FromForm] LoginVM model)
        {
            AccessToken token = _queryDispatcher.Dispatch<AccessToken>(_mapper.Map<LoginVM, LoginQuery>(model));
            return new JsonResult(token);
        }
    }
}
