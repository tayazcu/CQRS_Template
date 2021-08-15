using Project.Framework.Commands;

namespace Project.Core.Domain.Users.Commands
{
    public class AddClaimToUserCommand : ICommand
    {
        public long UserId { get; set; }
        public string ClaimValue { get; set; }
    }
}
