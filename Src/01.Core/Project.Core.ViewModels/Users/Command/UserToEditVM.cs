using Microsoft.AspNetCore.Http;
using Project.Core.Domain.Base;
using Project.Core.Resources.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.ViewModels.Users.Command
{
    public class UserToEditVM
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public string UserId { get; set; }

        /// <summary>
        /// نام فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianFirstName)]
        [DefaultValue("علی")]
        public string PersianFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianLastName)]
        [DefaultValue("نقوی")]
        public string PersianLastName { get; set; }

        /// <summary>
        /// نام انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishFirstName)]
        [DefaultValue("ali")]
        public string EnglishFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishLastName)]
        [DefaultValue("naghavi")]
        public string EnglishLastName { get; set; }

        /// <summary>
        /// نام پدر فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianFatherName)]
        [DefaultValue("رضا")]
        public string PersianFatherName { get; set; }

        /// <summary>
        /// نام پدر انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishFatherName)]
        [DefaultValue("reza")]
        public string EnglishFatherName { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        [Display(Name = SharedResource.DateOfBirth)]
        [DefaultValue("1990/05/15")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = SharedResource.NationalCode)]
        [DefaultValue("1190040181")]
        public string NationalCode { get; set; }

        /// <summary>
        /// محل تولد فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianPlaceOfBirth)]
        [DefaultValue("اصفهان")]
        public string PersianPlaceOfBirth { get; set; }

        /// <summary>
        /// محل تولد انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishPlaceOfBirth)]
        [DefaultValue("esfahan")]
        public string EnglishPlaceOfBirth { get; set; }

        /// <summary>
        /// جنسیت
        /// m=Male , f = Famale
        /// </summary>
        [Display(Name = SharedResource.Gender)]
        [DefaultValue("m for Male OR f for famale")]
        public string Gender { get; set; }

        /// <summary>
        /// تصویر پروفایل
        /// </summary>
        [Display(Name = SharedResource.ProfileImage)]
        [DefaultValue("Please select an image")]
        public IFormFile ProfileImage { get; set; }

        /// <summary>
        /// تاریخ صدور گذرنامه
        /// </summary>
        [Display(Name = SharedResource.DateOfPassportIssuance)]
        [DefaultValue("1990/05/15")]
        public DateTime? DateOfPassportIssuance { get; set; }

        /// <summary>
        /// تاریخ انقضای گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportExpirationDate)]
        [DefaultValue("1990/05/15")]
        public DateTime? PassportExpirationDate { get; set; }

        /// <summary>
        /// شماره گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportNumber)]
        [DefaultValue("a15236985")]
        public string PassportNumber { get; set; }

        /// <summary>
        /// تصویر گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportImage)]
        [DefaultValue("Please select an image")]
        public IFormFile PassportImage { get; set; }

        /// <summary>
        /// تصویر ویزا
        /// </summary>
        [Display(Name = SharedResource.VisaImage)]
        [DefaultValue("Please select an image")]
        public IFormFile VisaImage { get; set; }

        /// <summary>
        /// تلفن
        /// </summary>
        [Display(Name = SharedResource.PhoneNumber)]
        [DefaultValue("03153267550")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// موبایل
        /// </summary>
        [Display(Name = SharedResource.Mobile)]
        [DefaultValue("09217104520")]
        public string Mobile { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = SharedResource.Address)]
        [DefaultValue("اصفهان - خیابان مرداویج - کوچه بوستان - پلاک 178")]
        public string Address { get; set; }

        /// <summary>
        /// پست الکترونیک
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = SharedResource.Email)]
        [DefaultValue("mad.naghavi@gmail.com")]
        public string Email { get; set; }

        /// <summary>
        /// شماره واتساپ
        /// </summary>
        [Display(Name = SharedResource.WhatsAppNumber)]
        [DefaultValue("09217104520")]
        public string WhatsAppNumber { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = SharedResource.PostalCode)]
        [DefaultValue("86198159854")]
        public string PostalCode { get; set; }

        /// <summary>
        /// ایدی کاربر معرف
        /// </summary>
        [Display(Name = SharedResource.ReferralUserId)]
        [DefaultValue("شناسه کاربر معرف")]
        public long? ReferralUserId { get; set; }
    }
}
