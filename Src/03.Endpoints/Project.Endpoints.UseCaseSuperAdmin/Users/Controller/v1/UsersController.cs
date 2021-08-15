using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Core.ViewModels.Users.Query;
using Project.Endpoints.WebFramework;
using Project.Framework.ApiResultHellper;
using Project.Framework.Commands;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Endpoints.UseCaseSuperAdmin.Users.Controller.v1
{
    [ApiVersion("1")]
    [Authorize(policy: Policies.Admin)]
    public class UsersController : BaseAdminController
    {
        private readonly IMapper _mapper;
        public UsersController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager, IMapper mapper, IApiResultReturn apiResultReturn) : base(commandDispatcher, queryDispatcher, resourceManager, apiResultReturn)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId"> userId </param>
        /// <returns>user information</returns>
        /// <remarks>
        /// get one user access by userId
        /// </remarks> 
        [HttpGet("[action]/{userId}")]
        public IActionResult Get(string userId)
        {
            if (!userId.HasValue())
            {
                throw new ArgumentNullException(_resourceManager.GetName(SharedResource.Required, _resourceManager.GetName(SharedResource.UserName)));
            }
            UserToShowVM user = _queryDispatcher.Dispatch<UserToShowVM>(new GetUserQuery { UserId = userId });
            return _apiResultReturn.SetQuery(user);
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns>users information</returns>
        /// <remarks>
        /// get all user 
        /// </remarks> 
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            IEnumerable<UserToShowVM> users = _queryDispatcher.Dispatch<IEnumerable<UserToShowVM>>(new GetAllUserQuery());
            return _apiResultReturn.SetQuery(users);
        }

        /// <summary>
        /// add user
        /// </summary>
        /// <param name="model">user informations</param>
        /// <returns>result</returns>
        /// <remarks>
        /// add a user 
        /// </remarks>
        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Add(UserToAddVM model )
        {
            AddUserCommand command = _mapper.Map<UserToAddVM, AddUserCommand>(model);
            command.Roles = new RoleInfo
            {
                Role = Roles.User.ToString(),
                Status = TypeOfStatus.Active,
                roleDescription = $"this is {Roles.User} role"
            };
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }

        /// <summary>
        /// edit user
        /// </summary>
        /// <param name="model">user informations</param>
        /// <returns>result</returns>
        /// <remarks>
        /// edit a user 
        /// </remarks>
        [HttpPut("[action]")]
        //[ValidateAntiForgeryToken]
        public virtual IActionResult Edit(UserToEditVM model)
        {
            EditUserCommand command = _mapper.Map<UserToEditVM, EditUserCommand>(model);
            CommandResult result = _commandDispatcher.Dispatch(command);
            return _apiResultReturn.SetCommand(result);
        }

    }
}