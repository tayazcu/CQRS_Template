using FluentValidation;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;

namespace Project.Core.ViewModels.Users.Ruls
{
    public class UserClaimToAddValidator : AbstractValidator<UserClaimToAddVM>, IScopedDependency
    {
        private readonly IResourceManager _resourceManager;
        public UserClaimToAddValidator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            RuleFor(x => x.UserId)
                .NotNull()
                    .WithMessage(_resourceManager.GetName(SharedResource.UserId));

            RuleFor(x => x.ClaimValue)
                .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.ClaimValue)} {_resourceManager.GetName(SharedResource.Required)}");
        }
    }
}
