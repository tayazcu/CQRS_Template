using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.Core.CommandServices.Base;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Contracts.Users.ReadonlyRepositories;
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
    public class ChangeStatusUserRoleCommandHandler : CommandHandler<ChangeStatusUserRoleCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdateSecurityStampService _updateSecurityStampService;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        public ChangeStatusUserRoleCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork, UpdateSecurityStampService updateSecurityStampService,
                                                  IUserCommandRepository userCommandRepository,IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _identityCommandRepository = identityCommandCommandRepository;
            _unitOfWork = unitOfWork;
            _updateSecurityStampService = updateSecurityStampService;
        }

        private User selectedUser = null;
        private ChangeStatusUserRoleCommand command;
        public override CommandResult Handle(ChangeStatusUserRoleCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;

            GetUser( command.UserId);
            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.ChangeStatusFailure, _resourceManager.GetName(SharedResource.Role));

            _identityCommandRepository.ChangeStatusUserRole(selectedUser.Id, command.RoleName, command.Status);

            if (!_unitOfWork.CommitCahnges())
                return Failure(StatusCode.ServerError, SharedResource.ChangeStatusFailure, _resourceManager.GetName(SharedResource.Role));

            _updateSecurityStampService.Update(selectedUser);
            return Ok(SharedResource.ChangeStatusSuccess, _resourceManager.GetName(SharedResource.Role));
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
                if (!selectedUser.IsExist())
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
            selectedUser = _userCommandRepository.GetById(CancellationToken.None, id);
        }
    }
}
