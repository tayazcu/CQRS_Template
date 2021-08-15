using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Resources.Resources
{
    public class SharedResource
    {
        // model lengths
        public const string TextCouldNotBeLargerThan20000 = nameof(TextCouldNotBeLargerThan20000); // متن باید کمتر از 20000 کاراکتر باشد
        public const string RulesCouldNotBeLargerThan20000 = nameof(RulesCouldNotBeLargerThan20000); // قوانین باید کمتر از 20000 کاراکتر باشد
        public const string ExplanationCouldNotBeLargerThan20000 = nameof(ExplanationCouldNotBeLargerThan20000); // توضیح باید کمتر از 20000 کاراکتر باشد
        public const string DocumentationCouldNotBeLargerThan20000 = nameof(DocumentationCouldNotBeLargerThan20000); // مستندات باید کمتر از 20000 کاراکتر باشد
        public const string EmailIsNotValid = nameof(EmailIsNotValid); // پست الکترونیک معتبر نیست

        

        // main messages
        public const string RemoveSuccess = nameof(RemoveSuccess);
        public const string RemoveFailure = nameof(RemoveFailure);
        public const string ChangeStatusSuccess = nameof(ChangeStatusSuccess);
        public const string ChangeStatusFailure = nameof(ChangeStatusFailure);
        public const string AddFailure = nameof(AddFailure);
        public const string AddSuccess = nameof(AddSuccess);
        public const string EditSuccess = nameof(EditSuccess);
        public const string EditFailure = nameof(EditFailure);
        public const string BadRequest = nameof(BadRequest);
        public const string MustUseOAuthFlow = nameof(MustUseOAuthFlow);
        public const string UserOrPasswordNotCorrect = nameof(UserOrPasswordNotCorrect);
        public const string RoleIsInvalid = nameof(RoleIsInvalid);
        public const string ClaimIsInvalid = nameof(ClaimIsInvalid);

        // annotation
        public const string Required = nameof(Required);    //without parameter
        public const string IsRequired = nameof(IsRequired); //with parameter
        public const string Between2And250 = nameof(Between2And250);
        public const string Between2And500 = nameof(Between2And500);  // حداقل 2 کاراکتر و حداکثر 500 کاراکتر
        public const string Between2And50 = nameof(Between2And50);  // حداقل 2 کاراکتر و حداکثر 50 کاراکتر
        public const string Between10And200 = nameof(Between10And200);  // حداقل 10 کاراکتر و حداکثر 200 کاراکتر
        public const string MustBe9Characters = nameof(MustBe9Characters);  // باید 9 کارکتر باشد
        public const string MustBe10Characters = nameof(MustBe10Characters);  // باید 10 کارکتر باشد
        public const string MustBe1Characters = nameof(MustBe10Characters);  // باید 1 کارکتر باشد
        public const string MustBe11Characters = nameof(MustBe11Characters);  // باید 11 کارکتر باشد
        public const string Between2And200 = nameof(Between2And200);    // حداقل 2 کاراکتر و حداکثر 200 کاراکتر
        public const string Between7And400 = nameof(Between7And400);
        public const string Between7And256 = nameof(Between7And256);

        public const string IsExist = nameof(IsExist);
        public const string IsDuplicate = nameof(IsDuplicate);
        public const string NotFound = nameof(NotFound);
        public const string Unauthorized = nameof(Unauthorized);

        // database fields
        public const string PersianFirstName = nameof(PersianFirstName); // نام فارسی
        public const string PersianLastName = nameof(PersianLastName);   // نام خانوادگی فارسی
        public const string EnglishFirstName = nameof(EnglishFirstName); // نام انگلیسی
        public const string EnglishLastName = nameof(EnglishLastName); // نام خانوادگی انگلیسی
        public const string PersianFatherName = nameof(PersianFatherName); // نام پدر فارسی
        public const string EnglishFatherName = nameof(EnglishFatherName); // نام پدر انگلیسی
        public const string PersianPlaceOfBirth = nameof(PersianPlaceOfBirth); // محل تولد فارسی
        public const string EnglishPlaceOfBirth = nameof(EnglishPlaceOfBirth); // محل تولد انگلیسی
        public const string DateOfBirth = nameof(DateOfBirth); // تاریخ تولد
        public const string NationalCode = nameof(NationalCode); // کد ملی
        public const string Gender = nameof(Gender); //  جنسیت
        public const string ProfileImage = nameof(ProfileImage); //  تصویر پروفایل
        public const string DateOfPassportIssuance = nameof(DateOfPassportIssuance); //  تاریخ صدور گذرنامه
        public const string PassportExpirationDate = nameof(PassportExpirationDate); //  تاریخ انقضای گذرنامه
        public const string PassportNumber = nameof(PassportNumber); //  شماره گذرنامه
        public const string PassportImage = nameof(PassportImage); //  تصویر گذرنامه
        public const string VisaImage = nameof(VisaImage); //  تصویر ویزا
        public const string PhoneNumber = nameof(PhoneNumber); //  تلفن
        public const string Mobile = nameof(Mobile); //  موبایل
        public const string Address = nameof(Address); //  آدرس
        public const string WhatsAppNumber = nameof(WhatsAppNumber); //  شماره واتساپ
        public const string PostalCode = nameof(PostalCode); //  کد پستی
        public const string ReferralUserId = nameof(ReferralUserId); //  شناسه کاربر معرف
        public const string UserName = nameof(UserName);
        public const string Password = nameof(Password);
        public const string Email = nameof(Email);
        public const string Description = nameof(Description);  
        public const string User = nameof(User);    
        public const string EnglishName = nameof(EnglishName);  
        public const string RegisterDate = nameof(RegisterDate);  
        public const string PersianName = nameof(PersianName);  
        public const string UserType = nameof(UserType);  // نوع کاربر

        public const string Role = nameof(Role);    
        public const string ClaimValue = nameof(ClaimValue);    
        public const string Claim = nameof(Claim);    
        public const string ClaimType = nameof(ClaimType);    
        public const string UserId = nameof(UserId);    
        public const string Status = nameof(Status);   
        public const string GrantType = nameof(GrantType);   
        public const string MustBe8Characters = nameof(MustBe8Characters);   
        
        public const string TokenNoClaims = nameof(TokenNoClaims);    // This token has no claims.
        public const string TokenNoSecurityStamp = nameof(TokenNoSecurityStamp);    // This token has no security stamp.
        public const string TokenSecurityStampNotValid = nameof(TokenSecurityStampNotValid);    // Token security stamp is not valid.
        public const string AuthenticateFailure = nameof(AuthenticateFailure);    // Authenticate failure.
        public const string AuthenticateFailed = nameof(AuthenticateFailed);    // Authentication failed.
        public const string UnauthorizedToAccessResource = nameof(UnauthorizedToAccessResource);    // You are unauthorized to access this resource.
    }
}
