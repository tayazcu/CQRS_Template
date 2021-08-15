using Project.Core.ViewModels.Users.Query;
using System.Collections.Generic;

namespace Project.Core.Contracts.Users.ReadonlyRepositories
{
    public interface IIdentityReadonlyRepository
    {
        IEnumerable<string> GetAllUserClaim(long userId);
        IEnumerable<UserRoleToShowVM> GetAllUserRole(long userId);

    }
}
