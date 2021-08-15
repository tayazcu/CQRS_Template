using Microsoft.AspNetCore.Identity;
using Project.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Domain.Users.Entities
{
    public class User : IdentityUser<long>, IEntity
    {
        public User() : base()
        {

        }
        public DateTimeOffset? LastLoginDate { get; set; }

        public ICollection<UserInformation> ReferralUserInformations { get; set; }
        public  UserInformation UserInformation { get; set; }
    }
}
