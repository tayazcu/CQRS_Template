using Microsoft.AspNetCore.Identity;
using Project.Core.Domain.Base;
using Project.Framework.Domain;

namespace Project.Core.Domain.Users.Entities
{
    public class UserRole : IdentityUserRole<long>, IEntity
    {
        public UserRole() : base()
        {
            StatusType = TypeOfStatus.Pending;
        }

        public TypeOfStatus StatusType { get; set; }
    }
}
