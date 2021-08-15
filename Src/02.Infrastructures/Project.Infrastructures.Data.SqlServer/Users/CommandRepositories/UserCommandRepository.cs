using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Infrastructures.Data.SqlServer.Users.CommandRepositories
{
    public class UserCommandRepository : IUserCommandRepository, IScopedDependency, IDisposable
    {
        private readonly WriteContext _context;
        private readonly UserManager<User> _userManager;
        public UserCommandRepository(WriteContext applicationContext, UserManager<User> userManager)
        {
            _context = applicationContext;
            _userManager = userManager;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(User user)
        {
            Assert.NotNull(user, nameof(user));
            _context.Users.Add(user);
        }
        public void Edit(User user)
        {
            Assert.NotNull(user, nameof(user));
            _context.Users.Update(user);
        }
        public bool UserIsExist(string userName)
        {
            Assert.NotNull(userName, nameof(userName));

            User user = _userManager.FindByNameAsync(userName).Result;
            if (user.IsExist())
            {
                return true;
            }
            return false;
        }
        public bool EmailIsExist(long userId, string email)
        {
            Assert.NotNull(email, nameof(email));

            if (!userId.HasValue())
            {
                return _context.Users.AsNoTracking().Any(x => x.Email == email);
            }
            return _context.Users.AsNoTracking().Any(x => x.Email == email && x.Id != userId);
        }

        public User GetById(CancellationToken cancellationToken, long ids, bool ignoreQueryFilters = false)
        {
            if (ignoreQueryFilters)
                return _userManager.Users.IgnoreQueryFilters().SingleOrDefaultAsync(p => p.Id == ids).Result;
            return _userManager.Users.SingleOrDefaultAsync(p => p.Id == ids).Result;
            //return _userManager.FindByIdAsync(ids).Result;
        }
        public IdentityResult UpdateLastLoginDate(User user)
        {
            Assert.NotNull(user, nameof(user));

            user.LastLoginDate = DateTimeOffset.Now;
            IdentityResult result = _userManager.UpdateAsync(user).Result;
            return result;
        }
        public void UpdateSecurityStamp(User user, string securityStamp)
        {
            Assert.NotNull(user, nameof(user));

            user.SecurityStamp = securityStamp;
            _context.Users.Update(user);
        }
    }
}
