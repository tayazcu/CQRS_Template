using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Core.Contracts.Users.Services;
using Project.Endpoints.WebApi.Configuration;
using Project.Framework;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using Project.Infrastructures.Data.SqlServer.Common;
using System.Collections.Generic;
using System.Linq;

namespace Project.Endpoints.WebApi.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHsts(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Assert.NotNull(app, nameof(app));
            Assert.NotNull(env, nameof(env));

            if (!env.IsDevelopment())
                app.UseHsts();

            return app;
        }
        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app, SiteSettings siteSetting)
        {
            Assert.NotNull(app, nameof(app));

            List<string> uris = siteSetting.ApplicationSettings.CorsEnableUris;
            if (!uris.IsExist())
            {
                app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }
            else
            {
                app.UseCors(config => config
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .WithOrigins(uris.ToArray()));
            }

            return app;
        }
        public static IApplicationBuilder IntializeDatabase(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            using IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationContext dbContext = scope.ServiceProvider.GetService<ApplicationContext>(); //Service locator

            dbContext.Database.Migrate();

            List<IDataInitializer> dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>().ToList();
            foreach (IDataInitializer dataInitializer in dataInitializers.OrderBy(x => x.index))
                dataInitializer.InitializeData();

            return app;
        }
        public static IApplicationBuilder UseEndpoint(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            app.UseEndpoints(config =>
            {
                config.MapControllers().RequireAuthorization();          
                config.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

                //config.MapControllers();
                //config.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            return app;
        }
    }
}
