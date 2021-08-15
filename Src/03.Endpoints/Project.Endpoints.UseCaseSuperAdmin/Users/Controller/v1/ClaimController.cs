using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;

namespace Project.Endpoints.UseCaseSuperAdmin.Users.Controller.v1
{
    [ApiVersion("1")]
    [Authorize(policy: Policies.Admin)]
    public class ClaimController : BaseAdminController
    {
        private readonly IMapper _mapper;
        public ClaimController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IMapper mapper, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get all claims inside system
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult GetSystemClaims()
        {
            IEnumerable<string> roles = _queryDispatcher.Dispatch<IEnumerable<string>>(new GetAllSystemClaimQuery());
            return _apiResultReturn.SetQuery(roles);
        }

        /// <summary>
        /// Get all user claims 
        /// </summary>
        [HttpGet("[action]/{userId}")]
        public IActionResult GetAllUserClaims(string userId)
        {
            IEnumerable<string> list = _queryDispatcher.Dispatch<IEnumerable<string>>(new GetUserClaimsQuery { UserId = userId });
            return _apiResultReturn.SetQuery(list);
        }

        /// <summary>
        /// add claim to user
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>result</returns>
        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Add(UserClaimToAddVM model)
        {
            AddClaimToUserCommand command = _mapper.Map<UserClaimToAddVM, AddClaimToUserCommand>(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }

        /// <summary>
        /// remove claim from user
        /// </summary>
        /// <param name="model">model</param>
        /// <remarks>If not set Claim, all user claim will be deleted</remarks>
        [HttpDelete("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Remove(RemoveClaimFromUserVM model)
        {
            RemoveClaimFromUserCommand command = _mapper.Map<RemoveClaimFromUserVM, RemoveClaimFromUserCommand >(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }
    }
}