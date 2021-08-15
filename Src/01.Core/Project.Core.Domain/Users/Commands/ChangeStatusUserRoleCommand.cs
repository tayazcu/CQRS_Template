using Project.Core.Domain.Base;
using Project.Framework.Commands;

namespace Project.Core.Domain.Users.Commands
{
    public class ChangeStatusUserRoleCommand : ICommand
    {
        public long UserId { get; set; }
        public string RoleName { get; set; }
        public TypeOfStatus Status { get; set; }
    }
}
