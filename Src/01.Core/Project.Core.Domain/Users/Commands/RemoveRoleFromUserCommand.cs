using Project.Framework.Commands;

namespace Project.Core.Domain.Users.Commands
{
    public class RemoveRoleFromUserCommand : ICommand
    {
        public long UserId { get; set; }
        public string RoleName { get; set; }
    }
}
