using Microsoft.AspNetCore.Identity;
using Project.Core.Domain.Users.Entities;
using Project.Core.ViewModels.Users.Query;
using System.Collections.Generic;
using System.Threading;

namespace Project.Core.Contracts.Users.ReadonlyRepositories
{
    public interface IUserReadonlyRepository
    {
        IEnumerable<UserToShowVM> GetAll();
        public User Get(string username, bool ignoreQueryFilters = false);
        bool PasswordIsCorrect(User user, string password);
        User Get(long userId);
        void UpdateSecurityStamp(User user, string securityStamp);
    }
}
