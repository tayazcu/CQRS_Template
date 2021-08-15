using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.AutoMapper.Users;
using System;
using System.Linq;
using System.Reflection;

namespace Project.Endpoints.WebFramework.CustomMapping
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            //With AutoMapper Instance, you need to call AddAutoMapper services and pass assemblies that contains automapper Profile class
            //services.AddAutoMapper(assembly1, assembly2, assembly3);
            //See http://docs.automapper.org/en/stable/Configuration.html
            //And https://code-maze.com/automapper-net-core/

            services.AddAutoMapper(config =>
            {
                config.AddCustomMappingProfile();
                config.Advanced.BeforeSeal(configProvicer =>
                {
                    configProvicer.CompileMappings();
                });
            }, assemblies);

            #region Deprecated (Use AutoMapper Instance instead)
            //Mapper.Initialize(config =>
            //{
            //    config.AddCustomMappingProfile();
            //});

            ////Compile mapping after configuration to boost map speed
            //Mapper.Configuration.CompileMappings();
            #endregion
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
        {
            var profiles = typeof(UserInformationProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                config.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
    }
}
