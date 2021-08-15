using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Core.Contracts.Users.Services;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using System.Linq;

namespace Project.Core.CommandServices.Users
{
    public class RoleDataInitializerService : IDataInitializer, IScopedDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public RoleDataInitializerService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeData()
        {
            if (_userManager.Users.AsNoTracking().IgnoreQueryFilters().Any(p => p.UserName == DefaultValues.DefaultAdmin))
                return;

            _roleManager.CreateAsync(new Role { Name = "Admin" }).GetAwaiter().GetResult();
        }
        public int index { get; set; } = 1;
    }
}
