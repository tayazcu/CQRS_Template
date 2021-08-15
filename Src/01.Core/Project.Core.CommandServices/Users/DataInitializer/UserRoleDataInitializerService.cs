using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Contracts.Users.Services;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using System.Linq;

namespace Project.Core.CommandServices.Users
{
    public class UserRoleDataInitializerService : IDataInitializer, IScopedDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityCommandRepository _identityCommandCommandRepository;
        public UserRoleDataInitializerService(UserManager<User> userManager,   IUnitOfWork unitOfWork,IIdentityCommandRepository identityCommandCommandRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _identityCommandCommandRepository = identityCommandCommandRepository;
        }

        public void InitializeData()
        {
            User user = _userManager.Users.AsNoTracking().IgnoreQueryFilters().FirstOrDefault(p => p.UserName == DefaultValues.DefaultAdmin);
            if (user.IsExist())
                return;

            _identityCommandCommandRepository.AddRoleToUser(user.Id, "Admin", Domain.Base.TypeOfStatus.Active);
            _unitOfWork.Commit();
        }
        public int index { get; set; } = 3;
    }
}
