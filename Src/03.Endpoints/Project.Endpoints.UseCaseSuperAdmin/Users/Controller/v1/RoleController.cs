using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.CommandServices.Users;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Core.ViewModels.Users.Query;
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
    public class RoleController : BaseAdminController
    {
        private readonly IMapper _mapper;
        public RoleController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IMapper mapper, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get all roles inside system
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult GetSystemRoles()
        {
            IEnumerable<string> roles = _queryDispatcher.Dispatch<IEnumerable<string>>(new GetAllSystemRoleQuery());
            return _apiResultReturn.SetQuery(roles);
        }

        /// <summary>
        /// Get all user role 
        /// </summary>
        [HttpGet("[action]/{userId}")]
        public IActionResult GetAllUserRole(string userId)
        {
            IEnumerable<UserRoleToShowVM> list = _queryDispatcher.Dispatch<IEnumerable<UserRoleToShowVM>>(new GetUserRolesQuery { UserId = userId });
            return _apiResultReturn.SetQuery(list);
        }

        /// <summary>
        /// add role to user
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>result</returns>
        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Add(UserRoleToAddVM model)
        {
            AddRoleToUserCommand command = _mapper.Map<UserRoleToAddVM, AddRoleToUserCommand>(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }

        /// <summary>
        /// remove role from user
        /// </summary>
        /// <param name="model">model</param>
        /// <remarks>If not set RoleName, all user roles will be deleted</remarks>
        [HttpDelete("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Remove(RemoveRoleFromUserVM model)
        {
            RemoveRoleFromUserCommand command = _mapper.Map<RemoveRoleFromUserVM, RemoveRoleFromUserCommand>(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }

        /// <summary>
        /// change status user role  
        /// </summary>
        /// <param name="model">model</param>
        [HttpPut("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult ChangeStatus(UserRoleStatusToEditVM model)
        {
            ChangeStatusUserRoleCommand command = _mapper.Map<UserRoleStatusToEditVM, ChangeStatusUserRoleCommand>(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }
    }
}