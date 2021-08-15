using Autofac;
using Project.Core.AutoMapper.Users;
using Project.Core.CommandServices.Users;
using Project.Core.Contracts.Users.Services;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures;
using Project.Core.QueryServices.Users;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels;
using Project.Core.ViewModels.Users.Command;
using Project.Framework;
using Project.Framework.Commands;
using Project.Framework.DependencyInjection;
using Project.Framework.Queries;
using Project.Infrastructures.Data.SqlServer.Common;
using System.Reflection;

namespace Project.Endpoints.WebApi.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            Assembly frameworkAssembly = typeof(SiteSettings).Assembly;
            Assembly autoMapperAssembly = typeof(UserProfile).Assembly;
            Assembly contractsAssembly = typeof(IDataInitializer).Assembly;
            Assembly domainAssembly = typeof(User).Assembly;
            Assembly resourcesAssembly = typeof(SharedResource).Assembly;
            Assembly viewModelsAssembly = typeof(UserToAddVM).Assembly;
            Assembly sqlServerAssembly = typeof(UnitOfWork).Assembly;
            Assembly webFrameworkAssembly = typeof(ApplicationBuilderExtensions).Assembly;
            Assembly commandAssembly = typeof(AddUserCommandHandler).Assembly;
            Assembly queryAssembly = typeof(GetUserQueryHandler).Assembly;
            Assembly infrastructureAssembly = typeof(FileManagerServie).Assembly;

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly, commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
               .AssignableTo<IScopedDependencySingle>()
               .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AsClosedTypesOf(typeof(CommandHandler<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly,commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webFrameworkAssembly)
                .AssignableTo<ITransientDependencySingle>()
                .InstancePerDependency();

            //containerBuilder.RegisterAssemblyTypes(commandAssembly, queryAssembly, frameworkAssembly, autoMapperAssembly, contractsAssembly, domainAssembly, resourcesAssembly, viewModelsAssembly, sqlServerAssembly, webServiceAssembly, webFrameworkAssembly)
            //    .AssignableTo<IScopedDependencySingle>()
            //    .InstancePerLifetimeScope();
        }
    }
}
