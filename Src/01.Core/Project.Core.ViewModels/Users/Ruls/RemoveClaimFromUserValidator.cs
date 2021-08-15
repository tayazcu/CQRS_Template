using FluentValidation;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;

namespace Project.Core.ViewModels.Users.Ruls
{
    public class RemoveClaimFromUserValidator : AbstractValidator<RemoveClaimFromUserVM>, IScopedDependency
    {
        private readonly IResourceManager _resourceManager;
        public RemoveClaimFromUserValidator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            RuleFor(x => x.UserId)
                .NotNull()
                    .WithMessage(_resourceManager.GetName(SharedResource.UserId ));
        }
    }
}
