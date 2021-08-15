using Microsoft.AspNetCore.Http;
using Project.Framework.Commands;
using System;

namespace Project.Core.Domain.Users.Commands
{
    public class EditUserCommand : ICommand
    {
        public long UserId { get; set; }
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
        public long? ReferralUserId { get; set; }
    }
}
