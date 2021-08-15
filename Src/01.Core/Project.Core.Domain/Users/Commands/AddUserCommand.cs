using Microsoft.AspNetCore.Http;
using Project.Core.Domain.Base;
using Project.Core.Infrastructures.Identity;
using Project.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Domain.Users.Commands
{
    public class AddUserCommand : ICommand
    {
        public TypeOfUser UserType { get; set; }
        public string PersianFirstName { get; set; }
        public string PersianLastName { get; set; }
        public string EnglishFirstName { get; set; }
        public string EnglishLastName { get; set; }
        public string PersianFatherName { get; set; }
        public string EnglishFatherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NationalCode { get; set; }
        public string PersianPlaceOfBirth { get; set; }
        public string EnglishPlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public IFormFile ProfileImage { get; set; }
        public DateTime? DateOfPassportIssuance { get; set; }
        public DateTime? PassportExpirationDate { get; set; }
        public string PassportNumber { get; set; }
        public IFormFile PassportImage { get; set; }
        public IFormFile VisaImage { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string WhatsAppNumber { get; set; }
        public string PostalCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public long? ReferralUserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public RoleInfo Roles { get; set; }
    }
    public class RoleInfo
    {
        public string Role { get; set; }
        public string roleDescription { get; set; }
        public TypeOfStatus Status { get; set; }
    }
}
