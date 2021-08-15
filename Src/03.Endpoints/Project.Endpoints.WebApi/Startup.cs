using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Core.ViewModels.Users;
using Project.Core.ViewModels.Users.Ruls;
using Project.Endpoints.WebApi.Configuration;
using Project.Endpoints.WebFramework;
using Project.Endpoints.WebFramework.CustomMapping;
using Project.Endpoints.WebFramework.Identity;
using Project.Endpoints.WebFramework.Middlewares;
using Project.Endpoints.WebFramework.Swagger;
using Project.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Endpoints.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSetting;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration, _siteSetting);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddPolicy();

            services.AddMinimalMvc();

            services.AddElmahCore(Configuration, _siteSetting);

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger(_siteSetting, GetExecutiveRoute());
        }
        public static List<string> GetExecutiveRoute()
        {
            List<string> xmls = new List<string>(4);
            xmls.Add("ViewModels.xml");
            xmls.Add("UseCaseRoot.xml");
            xmls.Add("WebApi.xml");
            xmls.Add("UseCaseSuperAdmin.xml");
            return xmls;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseTimeHandler();

            app.IntializeDatabase();

            app.UseCustomExceptionHandler();

            app.ChangeResponseStatusHandler();

            //app.UseCustomAuthenticationHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseElmahCore(_siteSetting);

            app.UseSwaggerAndUI(_siteSetting);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAppCors(_siteSetting);

            app.UseEndpoint();
        }
    }
}
