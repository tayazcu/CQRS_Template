using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.Commands;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using System;
using System.Threading;

namespace Project.Core.CommandServices.Users
{
    public class EditUserCommandHandler : CommandHandler<EditUserCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IManageTransaction _manageTransaction;
        private readonly IUserInformationCommandRepository _userInformationCommandRepository;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork, IMapper mapper,
                                     IUserCommandRepository userCommandRepository, IPasswordHasher<User> passwordHasher, IManageTransaction manageTransaction,
                                     IUserInformationCommandRepository userInformationCommandRepository, IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _passwordHasher = passwordHasher;
            _manageTransaction = manageTransaction;
            _userInformationCommandRepository = userInformationCommandRepository;
            _identityCommandRepository = identityCommandCommandRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private UserInformation selectedUserInfo = null;
        private User selectedUser = null;
        private EditUserCommand command;
        bool savedChanges;
        public override CommandResult Handle(EditUserCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;

            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.EditFailure, _resourceManager.GetName(SharedResource.User));

            _manageTransaction.BeginTransaction();
            EditUser();
            EditUserInformation();
            if (!savedChanges)
            {
                _manageTransaction.RollbackTransaction();
                return Failure(StatusCode.BadRequest, SharedResource.EditFailure, _resourceManager.GetName(SharedResource.User));
            }
            //change files
            _manageTransaction.CommitTransaction();
            return Ok(SharedResource.AddSuccess, _resourceManager.GetName(SharedResource.User));
        }
        private bool IsValid()
        {
            bool isValid = true;

            //if (!command.UserId.HasValue())
            //{
            //    AddError(SharedResource.Required, SharedResource.FirstName);
            //    isValid = false;
            //}

            if (isValid)
            {
                selectedUser = _userCommandRepository.GetById(CancellationToken.None, command.UserId);
                if (!selectedUser.IsExist())
                {
                    AddError(SharedResource.NotFound, _resourceManager.GetName(SharedResource.User));
                    isValid = false;
                }

                selectedUserInfo = _userInformationCommandRepository.Get(command.UserId);
                if (!selectedUserInfo.IsExist())
                {
                    AddError(SharedResource.NotFound, _resourceManager.GetName(SharedResource.User));
                    isValid = false;
                }

                if (command.Email.HasValue() && _userCommandRepository.EmailIsExist(command.UserId, command.Email))
                {
                    AddError(SharedResource.IsDuplicate, command.Email);
                    isValid = false;
                }
            }

            return isValid;
        }
        private void EditUser()
        {
            selectedUser = _mapper.Map<EditUserCommand, User>(command, selectedUser);
            _userCommandRepository.Edit(selectedUser);
            savedChanges = _unitOfWork.CommitCahnges();
        }
        private void EditUserInformation()
        {
            selectedUserInfo = _mapper.Map<EditUserCommand, UserInformation>(command, selectedUserInfo);
            _userInformationCommandRepository.Edit(selectedUserInfo);
            savedChanges = _unitOfWork.CommitCahnges();
        }
    }
}
