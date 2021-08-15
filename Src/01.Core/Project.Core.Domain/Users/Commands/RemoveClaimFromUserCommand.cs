using Project.Framework.Commands;

namespace Project.Core.Domain.Users.Commands
{
    public class RemoveClaimFromUserCommand : ICommand
    {
        public long UserId { get; set; }
        public string Claim { get; set; }
    }
}
