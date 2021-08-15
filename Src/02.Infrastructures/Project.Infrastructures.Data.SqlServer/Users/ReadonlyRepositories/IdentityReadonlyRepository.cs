using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructures.Data.SqlServer.Users.ReadonlyRepositories
{
    public class IdentityReadonlyRepository : IIdentityReadonlyRepository, IScopedDependency, IDisposable
    {
        private readonly ReadContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IResourceManager _resourceManager;
        public IdentityReadonlyRepository(ReadContext applicationContext, UserManager<User> userManager, IResourceManager resourceManager)
        {
            _context = applicationContext;
            _userManager = userManager;
            _resourceManager = resourceManager;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<string> GetAllUserClaim(long userId)
        {
            return _context.UserClaims.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.ClaimValue);
        }
        public IEnumerable<UserRoleToShowVM> GetAllUserRole(long userId)
        {
            return (from ur in _context.UserRoles
                    join r in _context.Roles on ur.RoleId equals r.Id
                    where ur.UserId == userId
                    select new UserRoleToShowVM
                    {
                        Role = r.Name,
                        Status = ur.StatusType
                    }).AsNoTracking().ToList();
        }
    }
}
