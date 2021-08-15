using Microsoft.Extensions.DependencyInjection;
using Project.Core.Infrastructures.Identity;

namespace Project.Endpoints.WebFramework.Identity
{
    public static class PolicyConfigurationExtensions
    {
        public static void AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    //policy.RequireRole(Roles.Admin.ToString());
                });
            });
        }
    }
}
