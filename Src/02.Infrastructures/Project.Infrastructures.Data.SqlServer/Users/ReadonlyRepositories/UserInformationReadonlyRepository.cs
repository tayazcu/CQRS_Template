using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Linq;
using System.Threading;

namespace Project.Infrastructures.Data.SqlServer.Users.ReadonlyRepositories
{
    public class UserInformationReadonlyRepository : IUserInformationReadonlyRepository, IScopedDependency, IDisposable
    {
        private readonly ReadContext _context;
        public UserInformationReadonlyRepository(ReadContext applicationContext)
        {
            _context = applicationContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public UserInformation Get(long userId)
        {
            return _context.UserInformation.AsNoTracking().SingleOrDefault(x => x.UserId == userId);
        }
    }
}
