using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Entities;
using Project.Core.Infrastructures.Identity;
using System.Collections.Generic;

namespace Project.Core.Contracts.Users.CommandRepositories
{
    public interface IIdentityCommandRepository
    {
        void AddRoleToUser(long userId, string roleName, TypeOfStatus status);
        void RemoveRoleFromUser(long userId, string roleName);
        IEnumerable<UserRole> GetUserRoles(long userId);
        UserRole GetUserRole(long userId, long roleId);
        void ChangeStatusUserRole(long userId, string roleName, TypeOfStatus status);

        Role GetRoleByName(string roleName);
        Role GetRoleById(long roleId);
        Role AddRole(string roleName);

        void AddClaimToUser(long userId, string ClaimValue, string ClaimType);
        void RemoveClaimFromUser(long userId, string claimName);
        IEnumerable<UserClaim> GetUserClaims(long userId);
        UserClaim GetUserClaim(long userId, string claim);
    }
}
