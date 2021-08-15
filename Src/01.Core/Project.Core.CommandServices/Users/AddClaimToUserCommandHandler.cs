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
    public class AddClaimToUserCommandHandler : CommandHandler<AddClaimToUserCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        public AddClaimToUserCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork,
                                     IUserCommandRepository userCommandRepository,
                                     IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _identityCommandRepository = identityCommandCommandRepository;
            _unitOfWork = unitOfWork;
        }

        private string claimType = string.Empty;
        private long userId = 0;
        private AddClaimToUserCommand command;
        public override CommandResult Handle(AddClaimToUserCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;

            GetUser(command.UserId);
            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.ClaimValue));

            _identityCommandRepository.AddClaimToUser(userId, command.ClaimValue, claimType);

            if (!_unitOfWork.CommitCahnges())
                return Failure(StatusCode.BadRequest, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.ClaimValue));

            return Ok(SharedResource.AddSuccess, _resourceManager.GetName(SharedResource.ClaimValue));
        }
        private bool IsValid()
        {
            bool isValid = true;

            if (!command.UserId.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.UserId));
                isValid = false;
            }

            if (!command.ClaimValue.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.ClaimValue));
                isValid = false;
            }

            if (command.UserId.HasValue())
            {
                if (!userId.HasValue())
                {
                    AddError(SharedResource.NotFound, _resourceManager.GetName(SharedResource.User));
                    isValid = false;
                }
            }

            if (command.ClaimValue.HasValue())
            {
                IEnumerable<string> systemRoles = EnumExtension.ConvertToStringList<Claims>();
                string enumKey = systemRoles.Where(x => x.Equals(command.ClaimValue)).FirstOrDefault();
                if (!enumKey.HasValue())
                {
                    AddError(SharedResource.ClaimIsInvalid, command.ClaimValue);
                    isValid = false;
                }
                else
                {
                    claimType = enumKey.ToDescription<Claims>();
                }
            }
            
            return isValid;
        }
        private void GetUser(long id)
        {
            User user = _userCommandRepository.GetById(CancellationToken.None, id);
            if (user.IsExist())
            {
                userId = user.Id;
            }
        }
    }
}
