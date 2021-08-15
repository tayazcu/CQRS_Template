using Microsoft.AspNetCore.Identity;
using Project.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Domain.Users.Entities
{
    public class Role : IdentityRole<long>, IEntity
    {
        public Role() : base()
        {

        }
        public Role(string name) : base(name)
        {

        }
    }
}
