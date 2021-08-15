using FluentValidation;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;

namespace Project.Core.ViewModels.Users.Ruls
{
    public class UserToEditValidator : AbstractValidator<UserToEditVM>, IScopedDependency
    {
        private readonly IResourceManager _resourceManager;
        public UserToEditValidator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            RuleFor(x => x.PersianFirstName)
                .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianFirstName)} {_resourceManager.GetName(SharedResource.Required) }")
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianFirstName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.PersianLastName)
                .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianLastName)} {_resourceManager.GetName(SharedResource.Required) }")
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianLastName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.EnglishFirstName)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.EnglishFirstName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.EnglishLastName)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.EnglishLastName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.PersianFatherName)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianFatherName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.EnglishFatherName)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.EnglishFatherName)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.NationalCode)
                .Length(10, 10)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.NationalCode)} {_resourceManager.GetName(SharedResource.MustBe10Characters) }");

            RuleFor(x => x.PersianPlaceOfBirth)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PersianPlaceOfBirth)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.EnglishPlaceOfBirth)
                .Length(2, 200)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.EnglishPlaceOfBirth)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.Gender)
                .Length(1, 1)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.Gender)} {_resourceManager.GetName(SharedResource.MustBe1Characters) }");

            RuleFor(x => x.EnglishPlaceOfBirth)
               .Length(2, 200)
                   .WithMessage($"{_resourceManager.GetName(SharedResource.EnglishPlaceOfBirth)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.PassportNumber)
                .Length(9, 9)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PassportNumber)} {_resourceManager.GetName(SharedResource.MustBe9Characters) }");

            RuleFor(x => x.PhoneNumber)
                .Length(11, 11)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PhoneNumber)} {_resourceManager.GetName(SharedResource.MustBe11Characters) }");

            RuleFor(x => x.PostalCode)
                .Length(10, 10)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.PostalCode)} {_resourceManager.GetName(SharedResource.MustBe10Characters) }");

            RuleFor(x => x.WhatsAppNumber)
                .Length(11, 11)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.WhatsAppNumber)} {_resourceManager.GetName(SharedResource.MustBe11Characters) }");

            RuleFor(x => x.Mobile)
             .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.Mobile)} {_resourceManager.GetName(SharedResource.Required) }")
             .Length(11, 11)
                 .WithMessage($"{_resourceManager.GetName(SharedResource.Mobile)} {_resourceManager.GetName(SharedResource.MustBe11Characters) }");

            RuleFor(x => x.Address)
               .Length(2, 200)
                   .WithMessage($"{_resourceManager.GetName(SharedResource.Address)} {_resourceManager.GetName(SharedResource.Between2And200) }");

            RuleFor(x => x.Email)
             .Length(10, 200)
                 .WithMessage($"{_resourceManager.GetName(SharedResource.Mobile)} {_resourceManager.GetName(SharedResource.Between10And200) }");

            //RuleFor(x => x.ReferralUserId)
            // .Null()
            // .Length(2, 20)
            //     .WithMessage($"{_resourceManager.GetName(SharedResource.ReferralUserId)} {_resourceManager.GetName(SharedResource.Between10And200) }");

            //RuleFor(x => x.VisaImage.ToString())
            //    .Length(2, 500)
            //        .WithMessage($"{_resourceManager.GetName(SharedResource.VisaImage)} {_resourceManager.GetName(SharedResource.Between2And500) }");

            //RuleFor(x => x.PassportImage.ToString())
            //    .Length(2, 500)
            //        .WithMessage($"{_resourceManager.GetName(SharedResource.PassportImage)} {_resourceManager.GetName(SharedResource.Between2And500) }");

            //RuleFor(x => x.ProfileImage.ToString())
            //    .Length(2, 500)
            //        .WithMessage($"{_resourceManager.GetName(SharedResource.ProfileImage)} {_resourceManager.GetName(SharedResource.Between2And500) }");

            RuleFor(x => x.DateOfBirth.ToString())
                .Length(2, 50)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.DateOfBirth)} {_resourceManager.GetName(SharedResource.Between2And50) }");

            RuleFor(x => x.DateOfPassportIssuance.ToString())
               .Length(2, 250)
                   .WithMessage($"{_resourceManager.GetName(SharedResource.DateOfPassportIssuance)} {_resourceManager.GetName(SharedResource.Between2And50) }");

            RuleFor(x => x.PassportExpirationDate.ToString())
              .Length(2, 250)
                  .WithMessage($"{_resourceManager.GetName(SharedResource.PassportExpirationDate)} {_resourceManager.GetName(SharedResource.Between2And50) }");

            RuleFor(x => x.UserId)
               .NotNull()
                   .WithMessage($"{_resourceManager.GetName(SharedResource.UserId)} {_resourceManager.GetName(SharedResource.Required) }");
        }
    }
}
