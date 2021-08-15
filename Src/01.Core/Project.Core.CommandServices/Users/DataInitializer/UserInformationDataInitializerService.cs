using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Contracts.Users.Services;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using System;
using System.Linq;

namespace Project.Core.CommandServices.Users
{
    public class UserInformationDataInitializerService : IDataInitializer, IScopedDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInformationCommandRepository _userInformationCommandRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        public UserInformationDataInitializerService(UserManager<User> userManager, IUnitOfWork unitOfWork, IUserCommandRepository userCommandRepository,
                                                     IUserInformationCommandRepository userInformationCommandRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userCommandRepository = userCommandRepository;
            _userInformationCommandRepository = userInformationCommandRepository;
        }

        public void InitializeData()
        {
            User user = _userManager.Users.AsNoTracking().IgnoreQueryFilters().FirstOrDefault(p => p.UserName == DefaultValues.DefaultAdmin);
            if (user.IsExist())
                return;
            
            _userInformationCommandRepository.Add(new UserInformation
            {
                EnglishFirstName = "ali",
                PersianFirstName = "علی",
                EnglishLastName = "naghavi",
                PersianLastName = "نقوی",
                EnglishFatherName = "reza",
                PersianFatherName = "رضا",
                PersianPlaceOfBirth = "شهرضا",
                EnglishPlaceOfBirth = "shahreza",
                Address = "iran",
                Gender = "m",
                UserType = Domain.Base.TypeOfUser.Admin,
                RegisterDate = DateTime.Now,
                Email = "mad.naghavi@gmail.com",
                Mobile = "09217104520",
                NationalCode = "1190040182",
                PhoneNumber = "09217104520",
                PostalCode = "8619814571",
                UserId = user.Id
            });

            _unitOfWork.Commit();
        }
        public int index { get; set; } = 8;
    }
}
