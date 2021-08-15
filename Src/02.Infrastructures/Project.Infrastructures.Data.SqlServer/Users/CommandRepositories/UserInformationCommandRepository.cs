using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Linq;
using System.Threading;

namespace Project.Infrastructures.Data.SqlServer.Users.CommandRepositories
{
    public class UserInformationCommandRepository : IUserInformationCommandRepository, IScopedDependency, IDisposable
    {
        private readonly WriteContext _context;
        public UserInformationCommandRepository(WriteContext applicationContext)
        {
            _context = applicationContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(UserInformation user)
        {
            Assert.NotNull(user, nameof(user));
            _context.UserInformation.Add(user);
        }
        public void Edit(UserInformation user)
        {
            Assert.NotNull(user, nameof(user));
            _context.UserInformation.Update(user);
        }
        public UserInformation Get(long userId)
        {
            return _context.UserInformation.AsNoTracking().SingleOrDefault(x => x.UserId == userId);
        }
    }
}
