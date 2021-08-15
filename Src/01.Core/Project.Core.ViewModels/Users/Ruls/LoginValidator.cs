using FluentValidation;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;

namespace Project.Core.ViewModels.Users.Ruls
{
    public class LoginValidator : AbstractValidator<LoginVM>, IScopedDependency
    {
        private readonly IResourceManager _resourceManager;
        public LoginValidator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            RuleFor(x => x.grant_type)
                .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.GrantType)} {_resourceManager.GetName(SharedResource.Required) }")
                .Length(8, 8)
                    .WithMessage($"{_resourceManager.GetName(SharedResource.GrantType)} {_resourceManager.GetName(SharedResource.MustBe8Characters) }")
                 .When(x=>x.grant_type !="password")
                    .WithMessage($"{_resourceManager.GetName(SharedResource.GrantType)} {_resourceManager.GetName(SharedResource.MustUseOAuthFlow) }");

            RuleFor(x => x.username)
               .NotNull()
                   .WithMessage($"{_resourceManager.GetName(SharedResource.UserName)} {_resourceManager.GetName(SharedResource.Required) }")
               .Length(2, 250)
                   .WithMessage($"{_resourceManager.GetName(SharedResource.UserName)} {_resourceManager.GetName(SharedResource.Between7And256) }");

            RuleFor(x => x.password)
               .NotNull()
                   .WithMessage($"{_resourceManager.GetName(SharedResource.Password)} {_resourceManager.GetName(SharedResource.Required) }")
               .Length(2, 250)
                   .WithMessage($"{_resourceManager.GetName(SharedResource.Password)} {_resourceManager.GetName(SharedResource.Between7And256) }");
        }
    }
}
