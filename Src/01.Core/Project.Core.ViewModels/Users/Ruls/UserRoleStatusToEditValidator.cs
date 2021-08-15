using FluentValidation;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Command;
using Project.Framework.DependencyInjection;
using Project.Framework.Resources;

namespace Project.Core.ViewModels.Users.Ruls
{
    public class UserRoleStatusToEditValidator : AbstractValidator<UserRoleStatusToEditVM>, IScopedDependency
    {
        private readonly IResourceManager _resourceManager;
        public UserRoleStatusToEditValidator(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            RuleFor(x => x.UserId)
                .NotNull()
                    .WithMessage(_resourceManager.GetName(SharedResource.UserId));

            RuleFor(x => x.RoleName)
                .NotNull()
                    .WithMessage($"{_resourceManager.GetName(SharedResource.Role)} {_resourceManager.GetName(SharedResource.Required)}");

            RuleFor(x => x.Status)
               .NotNull()
                   .WithMessage($"{_resourceManager.GetName(SharedResource.Status)} {_resourceManager.GetName(SharedResource.Required)}");
        }
    }
}
