using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Contracts.Users.Services;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.CommandServices.Users
{
    public class UserDataInitializerService : IDataInitializer, IScopedDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommandRepository _userCommandRepository;
        public UserDataInitializerService(UserManager<User> userManager,  IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork,IUserCommandRepository userCommandRepository)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userCommandRepository = userCommandRepository;
        }

        public void InitializeData()
        {
            if (_userManager.Users.AsNoTracking().IgnoreQueryFilters().Any(p => p.UserName == DefaultValues.DefaultAdmin))
                return;

            User user = new User
            {
                UserName = DefaultValues.DefaultAdmin,
                NormalizedUserName = DefaultValues.DefaultAdmin.ToUpper(),

                Email = "Admin@Gmail.Com",
                EmailConfirmed = false,
                NormalizedEmail = "Admin@Gmail.Com".ToUpper(),

                PhoneNumber = "09217104520",
                PhoneNumberConfirmed = false,

                LockoutEnabled = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                AccessFailedCount = 0,

                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            string passwordHash = _passwordHasher.HashPassword(user, "123456789");
            user.PasswordHash = passwordHash;
            _userCommandRepository.Add(user);
            _unitOfWork.Commit();
        }
        public int index { get; set; } = 2;
    }
}
