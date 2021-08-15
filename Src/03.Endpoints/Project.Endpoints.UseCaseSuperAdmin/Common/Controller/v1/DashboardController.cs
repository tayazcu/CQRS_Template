using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users;
using Project.Endpoints.WebFramework;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Extensions;
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
    [Authorize(policy: Policies.Admin)]
    public class DashboardController : BaseAdminController
    {
        private readonly IMapper _mapper;
        public DashboardController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IMapper mapper, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            return Ok("OK");
        }
    }
}
