using Project.Core.Domain.Base;
using Project.Core.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ViewModels.Users.Query
{
    public class UserToShowVM
    {
        /// <summary>
        /// شناسه
        /// </summary>
        [Display(Name = SharedResource.UserId)]
        public long UserId { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = SharedResource.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        [Display(Name = SharedResource.RegisterDate)]
        public string RegisterDate { get; set; }

        /// <summary>
        /// نوع کاربر
        /// </summary>
        [Display(Name = SharedResource.UserType)]
        public string UserType { get; set; }        // enum to string

        /// <summary>
        /// نام فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianFirstName)]
        public string PersianFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianLastName)]
        public string PersianLastName { get; set; }

        /// <summary>
        /// نام انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishFirstName)]
        public string EnglishFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishLastName)]
        public string EnglishLastName { get; set; }

        /// <summary>
        /// نام پدر فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianFatherName)]
        public string PersianFatherName { get; set; }

        /// <summary>
        /// نام پدر انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishFatherName)]
        public string EnglishFatherName { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        [Display(Name = SharedResource.DateOfBirth)]
        public string DateOfBirth { get; set; }  // convert date time to string

        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = SharedResource.NationalCode)]
        public string NationalCode { get; set; }

        /// <summary>
        /// محل تولد فارسی
        /// </summary>
        [Display(Name = SharedResource.PersianPlaceOfBirth)]
        public string PersianPlaceOfBirth { get; set; }

        /// <summary>
        /// محل تولد انگلیسی
        /// </summary>
        [Display(Name = SharedResource.EnglishPlaceOfBirth)]
        public string EnglishPlaceOfBirth { get; set; }

        /// <summary>
        /// جنسیت
        /// m=Male , f = Famale
        /// </summary>
        [Display(Name = SharedResource.Gender)]
        public string Gender { get; set; }

        /// <summary>
        /// تصویر پروفایل
        /// </summary>
        [Display(Name = SharedResource.ProfileImage)]
        public string ProfileImage { get; set; }    // url

        /// <summary>
        /// تاریخ صدور گذرنامه
        /// </summary>
        [Display(Name = SharedResource.DateOfPassportIssuance)]
        public DateTime? DateOfPassportIssuance { get; set; }

        /// <summary>
        /// تاریخ انقضای گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportExpirationDate)]
        public string  PassportExpirationDate { get; set; }   // convert date time to string

        /// <summary>
        /// شماره گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportNumber)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// تصویر گذرنامه
        /// </summary>
        [Display(Name = SharedResource.PassportImage)]
        public string PassportImage { get; set; }    // url

        /// <summary>
        /// تصویر ویزا
        /// </summary>
        [Display(Name = SharedResource.VisaImage)]
        public string VisaImage { get; set; }    // url

        /// <summary>
        /// تلفن
        /// </summary>
        [Display(Name = SharedResource.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// موبایل
        /// </summary>
        [Display(Name = SharedResource.Mobile)]
        public string Mobile { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = SharedResource.Address)]
        public string Address { get; set; }

        /// <summary>
        /// پست الکترونیک
        /// </summary>
        [Display(Name = SharedResource.Email)]
        public string Email { get; set; }

        /// <summary>
        /// شماره واتساپ
        /// </summary>
        [Display(Name = SharedResource.WhatsAppNumber)]
        public string WhatsAppNumber { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = SharedResource.PostalCode)]
        public string PostalCode { get; set; }
    }
}
