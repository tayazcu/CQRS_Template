using Microsoft.AspNetCore.Identity;
using Project.Framework.Domain;

namespace Project.Core.Domain.Users.Entities
{
    public class UserClaim : IdentityUserClaim<long>, IEntity
    {
        public UserClaim() : base()
        {

        }

    }
}
