using Microsoft.AspNetCore.Identity;
using Project.Core.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Contracts.Users.CommandRepositories
{
    public interface IUserCommandRepository
    {
        void Add(User model);
        void Edit(User user);
        bool UserIsExist(string userName);
        bool EmailIsExist(long userId, string email);

        IdentityResult UpdateLastLoginDate(User user);
        User GetById(CancellationToken cancellationToken, long ids, bool ignoreQueryFilters = false);
        void UpdateSecurityStamp(User user, string securityStamp);
    }
}
