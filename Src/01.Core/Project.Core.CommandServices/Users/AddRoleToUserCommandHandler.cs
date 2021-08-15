using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.Commands;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Project.Core.CommandServices.Users
{
    public class AddRoleToUserCommandHandler : CommandHandler<AddRoleToUserCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        public AddRoleToUserCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork,
                                     IUserCommandRepository userCommandRepository,
                                     IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _identityCommandRepository = identityCommandCommandRepository;
            _unitOfWork = unitOfWork;
        }

        private long userId = 0;
        private AddRoleToUserCommand command;
        public override CommandResult Handle(AddRoleToUserCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;
            GetUser(command.UserId);
            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.Role));

            _identityCommandRepository.AddRoleToUser(userId, command.RoleName, command.Status);

            if (!_unitOfWork.CommitCahnges())
                return Failure(StatusCode.ServerError, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.Role));

            return Ok(SharedResource.AddSuccess, _resourceManager.GetName(SharedResource.Role));
        }
        private bool IsValid()
        {
            bool isValid = true;

            if (!command.UserId.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.UserId));
                isValid = false;
            }

            if (!command.RoleName.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.Role));
                isValid = false;
            }

            if (isValid)
            {
                if (!userId.HasValue())
                {
                    AddError(SharedResource.NotFound, _resourceManager.GetName(SharedResource.User));
                    isValid = false;
                }

                IEnumerable<string> systemRoles = EnumExtension.ConvertToStringList<Roles>();
                if (!systemRoles.Any(x => x.Equals(command.RoleName)))
                {
                    AddError(SharedResource.RoleIsInvalid, command.RoleName);
                    isValid = false;
                }
            }
            return isValid;
        }
        private void GetUser(long id)
        {
            User user = _userCommandRepository.GetById(CancellationToken.None, id);
            if (user.IsExist())
                userId = user.Id;
        }

    }
}
