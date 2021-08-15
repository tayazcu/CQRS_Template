using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Project.Infrastructures.Data.SqlServer.Users.ReadonlyRepositories
{
    public class UserReadonlyRepository : IUserReadonlyRepository, IScopedDependency, IDisposable
    {
        private readonly ReadContext _context;
        private readonly UserManager<User> _userManager;
        public UserReadonlyRepository(ReadContext applicationContext, UserManager<User> userManager)
        {
            _context = applicationContext;
            _userManager = userManager;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public User Get(string username, bool ignoreQueryFilters = false)
        {
            Assert.NotNull(username, nameof(username));

            if (ignoreQueryFilters)
                return _userManager.Users.AsNoTracking().IgnoreQueryFilters().SingleOrDefaultAsync(p => p.UserName == username).Result;
            return _userManager.Users.AsNoTracking().SingleOrDefaultAsync(p => p.UserName == username).Result;
        }
        public User Get(long userId)
        {
            return _userManager.Users.AsNoTracking().SingleOrDefaultAsync(p => p.Id == userId).Result;
        }
        public IEnumerable<UserToShowVM> GetAll()
        {
            return (from u in _context.Users
                    join ui in _context.UserInformation on u.Id equals ui.UserId
                    select new UserToShowVM
                    {
                        UserId = u.Id,
                        Email = u.Email,
                        Address = ui.Address,
                        UserType = ui.UserType.ToDisplay(DisplayProperty.Name),
                        Mobile = ui.Mobile,
                        DateOfBirth = ui.DateOfBirth.ToShortDateStringShahmsi(),
                        DateOfPassportIssuance = ui.DateOfPassportIssuance,
                        EnglishFatherName = ui.EnglishFatherName,
                        EnglishFirstName = ui.EnglishFirstName,
                        EnglishLastName = ui.EnglishLastName,
                        EnglishPlaceOfBirth = ui.EnglishPlaceOfBirth,
                        Gender = ui.Gender,
                        NationalCode = ui.NationalCode,
                        PassportExpirationDate = ui.PassportExpirationDate.ToShortDateStringShahmsi(),
                        PassportImage = ui.PassportImage,
                        PassportNumber = ui.PassportNumber,
                        PersianFatherName = ui.PersianFatherName,
                        PersianFirstName = ui.PersianFirstName,
                        PersianLastName = ui.PersianLastName,
                        PersianPlaceOfBirth = ui.PersianPlaceOfBirth,
                        PhoneNumber = ui.PhoneNumber,
                        PostalCode = ui.PostalCode,
                        ProfileImage = ui.ProfileImage,
                        VisaImage = ui.VisaImage,
                        WhatsAppNumber = ui.WhatsAppNumber,
                        UserName = u.UserName,
                        RegisterDate = ui.RegisterDate.ToShortDateStringShahmsi()
                    }).AsNoTracking().ToList();

        }
        public bool PasswordIsCorrect(User user, string password)
        {
            Assert.NotNull(user, nameof(user));
            Assert.NotNull(password, nameof(password));

            return _userManager.CheckPasswordAsync(user, password).Result;
        }
        public void UpdateSecurityStamp(User user, string securityStamp)
        {
            Assert.NotNull(user, nameof(user));

            user.SecurityStamp = securityStamp;
            _context.Users.Update(user);
        }
    }
}
