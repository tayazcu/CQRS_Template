using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.Core.CommandServices.Base;
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
    public class RemoveRoleFromUserCommandHandler : CommandHandler<RemoveRoleFromUserCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly UpdateSecurityStampService _updateSecurityStampService;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        public RemoveRoleFromUserCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork,
                                     IUserCommandRepository userCommandRepository, UpdateSecurityStampService updateSecurityStampService,
                                     IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _updateSecurityStampService = updateSecurityStampService;
            _identityCommandRepository = identityCommandCommandRepository;
            _unitOfWork = unitOfWork;
        }

        private User selectedUser = null;
        private string roleId = string.Empty;
        private RemoveRoleFromUserCommand command;
        public override CommandResult Handle(RemoveRoleFromUserCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;

            GetUser(command.UserId);
            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.RemoveFailure, _resourceManager.GetName(SharedResource.Role));

            _identityCommandRepository.RemoveRoleFromUser(selectedUser.Id, command.RoleName);

            if (!_unitOfWork.CommitCahnges())
                return Failure(StatusCode.ServerError, SharedResource.RemoveFailure, _resourceManager.GetName(SharedResource.Role));

            _updateSecurityStampService.Update(selectedUser);
            return Ok(SharedResource.RemoveSuccess, _resourceManager.GetName(SharedResource.Role));
        }
        private bool IsValid()
        {
            bool isValid = true;

            if (!command.UserId.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.UserId));
                isValid = false;
            }

            if (isValid)
            {
                if (!selectedUser.IsExist())
                {
                    AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.UserId));
                    isValid = false;
                }

                if (command.RoleName.HasValue())
                {
                    IEnumerable<string> systemRoles = EnumExtension.ConvertToStringList<Roles>();
                    if (!systemRoles.Any(x => x.Equals(command.RoleName)))
                    {
                        AddError(SharedResource.RoleIsInvalid, command.RoleName);
                        isValid = false;
                    }
                }
            }
            return isValid;
        }
        private void GetUser(long id)
        {
            selectedUser = _userCommandRepository.GetById(CancellationToken.None, id);
        }
    }
}
