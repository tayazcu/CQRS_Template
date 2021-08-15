using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.Commands;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Core.CommandServices.Users
{
    public class AddUserCommandHandler : CommandHandler<AddUserCommand>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserInformationCommandRepository _userInformationCommandRepository;
        private readonly IIdentityCommandRepository _identityCommandRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IManageTransaction _manageTransaction;

        public AddUserCommandHandler(IResourceManager resourceManager, IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager,
                                     IUserCommandRepository userCommandRepository, IPasswordHasher<User> passwordHasher, IManageTransaction manageTransaction,
                                     IUserInformationCommandRepository userInformationCommandRepository, IIdentityCommandRepository identityCommandCommandRepository) : base(resourceManager)
        {
            _manageTransaction = manageTransaction;
            _resourceManager = resourceManager;
            _userCommandRepository = userCommandRepository;
            _passwordHasher = passwordHasher;
            _userInformationCommandRepository = userInformationCommandRepository;
            _identityCommandRepository = identityCommandCommandRepository;
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        private User user = null;
        private AddUserCommand command;
        bool savedChanges;
        public override CommandResult Handle(AddUserCommand command)
        {
            Assert.NotNull(command, nameof(command));
            this.command = command;

            if (!IsValid())
                return Failure(StatusCode.BadRequest, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.User));

            _manageTransaction.BeginTransaction();
            AddUser();
            AddUserInformation();
            AddRoleUoUser();
            if (!savedChanges)
            {
                _manageTransaction.RollbackTransaction();
                return Failure(StatusCode.BadRequest, SharedResource.AddFailure, _resourceManager.GetName(SharedResource.User));
            }
            //pload files
            _manageTransaction.CommitTransaction();
            return Ok(SharedResource.AddSuccess, _resourceManager.GetName(SharedResource.User));

        }
        private bool IsValid()
        {
            bool isValid = true;

            if (!command.UserName.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.UserName));
                isValid = false;
            }

            if (!command.Password.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.Password));
                isValid = false;
            }

            if (!command.Email.HasValue())
            {
                AddError(SharedResource.IsRequired, _resourceManager.GetName(SharedResource.Email));
                isValid = false;
            }

            if (isValid)
            {
                if (_userCommandRepository.UserIsExist(command.UserName))
                {
                    AddError(SharedResource.IsExist, command.UserName);
                    isValid = false;
                }
                if (_userCommandRepository.EmailIsExist(0, command.Email))
                {
                    AddError(SharedResource.IsDuplicate, command.Email);
                    isValid = false;
                }
            }

            return isValid;
        }
        private void AddUser()
        {
            user = new User
            {
                NormalizedEmail = command.Email.ToUpper(),
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                NormalizedUserName = command.UserName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = command.Email,
                UserName = command.UserName
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);
            _userCommandRepository.Add(user);
            savedChanges = _unitOfWork.CommitCahnges();
        }
        private void AddRoleUoUser()
        {
            if (!command.Roles.IsExist())
                return;

            _identityCommandRepository.AddRoleToUser(user.Id, command.Roles.Role, command.Roles.Status);
            savedChanges = _unitOfWork.CommitCahnges();
        }
        private void AddUserInformation()
        {
            UserInformation userInformation = _mapper.Map<AddUserCommand, UserInformation>(command);
            userInformation.UserId = user.Id;
            _userInformationCommandRepository.Add(userInformation);
            savedChanges = _unitOfWork.CommitCahnges();
        }
    }
}
