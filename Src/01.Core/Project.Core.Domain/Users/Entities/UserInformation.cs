using Project.Core.Domain.Base;
using Project.Core.Resources.Resources;
using Project.Framework.Domain;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using System;

namespace Project.Core.Domain.Users.Entities
{
    public class UserInformation : BaseEntity<long>, IEntity
    {
        public UserInformation()
        {
            UserType = TypeOfUser.Customer;
            RegisterDate = DateTime.Now;
        }

        /// <summary>
        /// نوع کاربر
        /// </summary>
        public TypeOfUser UserType { get; set; }

        /// <summary>
        /// نام فارسی
        /// </summary>
        public string PersianFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی فارسی
        /// </summary>
        public string PersianLastName { get; set; }

        /// <summary>
        /// نام انگلیسی
        /// </summary>
        public string EnglishFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی انگلیسی
        /// </summary>
        public string EnglishLastName { get; set; }

        /// <summary>
        /// نام پدر فارسی
        /// </summary>
        public string PersianFatherName { get; set; }

        /// <summary>
        /// نام پدر انگلیسی
        /// </summary>
        public string EnglishFatherName { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }

        /// <summary>
        /// محل تولد فارسی
        /// </summary>
        public string PersianPlaceOfBirth { get; set; }

        /// <summary>
        /// محل تولد انگلیسی
        /// </summary>
        public string EnglishPlaceOfBirth { get; set; }

        /// <summary>
        /// جنسیت
        /// m=Male , f = Famale
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// تصویر پروفایل
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// تاریخ صدور گذرنامه
        /// </summary>
        public DateTime? DateOfPassportIssuance { get; set; }

        /// <summary>
        /// تاریخ انقضای گذرنامه
        /// </summary>
        public DateTime? PassportExpirationDate { get; set; }

        /// <summary>
        /// شماره گذرنامه
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// تصویر گذرنامه
        /// </summary>
        public string PassportImage { get; set; }

        /// <summary>
        /// تصویر ویزا
        /// </summary>
        public string VisaImage { get; set; }

        /// <summary>
        /// تلفن
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// موبایل
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// پست الکترونیک
        /// </summary>
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                if (!email.EmailIsValid())
                    throw new BadRequestException(SharedResource.EmailIsNotValid);
            }
        }

        /// <summary>
        /// شماره واتساپ
        /// </summary>
        public string WhatsAppNumber { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// ایدی کاربر معرف
        /// </summary>
        public long? ReferralUserId { get; set; }
        public User ReferralUser { get; set; }

        /// <summary>
        /// ایدی کاربر
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
