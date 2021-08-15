using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Entities;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Framework.DependencyInjection;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Infrastructures.Data.SqlServer.Users.CommandRepositories
{
    // _context.Entry<Role>(role).Reload();
    public class IdentityCommandRepository : IIdentityCommandRepository, IScopedDependency, IDisposable
    {
        private readonly WriteContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IResourceManager _resourceManager;
        public IdentityCommandRepository(WriteContext applicationContext, UserManager<User> userManager, IResourceManager resourceManager)
        {
            _context = applicationContext;
            _userManager = userManager;
            _resourceManager = resourceManager;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddRoleToUser(long userId, string roleName, TypeOfStatus status)
        {
            Assert.NotNull(roleName, nameof(roleName));

            Role role = GetRoleByName(roleName);
            if (!role.IsExist())
            {
                role = AddRole(roleName);
            }

            _context.UserRoles.Add(new UserRole
            {
                RoleId = role.Id,
                StatusType = status,
                UserId = userId
            });
        }
        public void ChangeStatusUserRole(long userId, string roleName, TypeOfStatus status)
        {
            Assert.NotNull(roleName, nameof(roleName));

            Role role = GetRoleByName(roleName);
            if (!role.IsExist())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.Role, SharedResource.NotFound));

            UserRole userRole = GetUserRole(userId, role.Id);
            userRole.StatusType = status;
        }
        public void RemoveRoleFromUser(long userId, string roleName)
        {
            if (!roleName.HasValue())
            {
                _context.UserRoles.RemoveRange(GetUserRoles(userId));
                return;
            }

            UserRole userRole = GetUserRole(userId, GetRoleByName(roleName).Id);
            if (userRole.IsExist())
            {
                _context.UserRoles.Remove(userRole);
            }
        }
        public IEnumerable<UserRole> GetUserRoles(long userId)
        {
            if (!UserIsExist(userId))
                throw new NotFoundException(_resourceManager.GetName(SharedResource.User, SharedResource.NotFound));

            return _context.UserRoles.Where(x => x.UserId == userId);
        }
        public UserRole GetUserRole(long userId, long roleId)
        {
            if (!UserIsExist(userId))
                throw new NotFoundException(_resourceManager.GetName(SharedResource.User, SharedResource.NotFound));

            Role role = GetRoleById(roleId);
            if (!role.IsExist())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.Role, SharedResource.NotFound));

            return _context.UserRoles.SingleOrDefault(x => x.UserId == userId && x.RoleId == roleId);
        }

        public Role GetRoleByName(string roleName)
        {
            return _context.Roles.AsNoTracking().SingleOrDefault(x => x.Name.Equals(roleName));
        }
        public Role GetRoleById(long roleId)
        {
            return _context.Roles.AsNoTracking().SingleOrDefault(x => x.Id.Equals(roleId));
        }
        public Role AddRole(string roleName)
        {
            Role role = new Role
            {
                //Id = Guid.NewGuid().ToString(),
                Name = roleName.ToString(),
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            _context.Roles.Add(role);
            return role;
        }

        public void AddClaimToUser(long userId, string ClaimValue, string ClaimType)
        {
            Assert.NotNull(ClaimValue, nameof(ClaimValue));
            Assert.NotNull(ClaimType, nameof(ClaimType));

            _context.UserClaims.Add(new UserClaim
            {
                ClaimValue = ClaimValue,
                ClaimType = ClaimType,
                UserId = userId
            });
        }
        public void RemoveClaimFromUser(long userId, string claimName)
        {
            if (!claimName.ToString().HasValue())
            {
                _context.UserClaims.RemoveRange(GetUserClaims(userId));
                return;
            }

            UserClaim userClaim = GetUserClaim(userId, claimName);
            if (userClaim.IsExist())
            {
                _context.UserClaims.Remove(userClaim);
            }
        }
        public IEnumerable<UserClaim> GetUserClaims(long userId)
        {
            if (!UserIsExist(userId))
                throw new NotFoundException(_resourceManager.GetName(SharedResource.User, SharedResource.NotFound));

            return _context.UserClaims.Where(x => x.UserId == userId);
        }
        public UserClaim GetUserClaim(long userId, string claim)
        {
            Assert.NotNull(claim, nameof(claim));

            return _context.UserClaims.SingleOrDefault(x => x.UserId == userId && x.ClaimValue == claim);
        }

        private bool UserIsExist(long id)
        {
            if (_context.Users.AsNoTracking().Any(x => x.Id == id))
                return true;
            return false;
        }
    }
}
