using ElmahCore.Mvc;
using ElmahCore.Sql;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Core.Resources.Resources;
using Project.Endpoints.WebFramework.Attributes;
using Project.Framework;
using Project.Framework.Domain;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Resources;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Endpoints.WebApi.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
             {
                 builder.AddSimpleConsole(x =>
                 {
                     x.ColorBehavior = LoggerColorBehavior.Enabled;
                 });
             });
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {

            services.AddScoped((serviceProvider) =>
            {
                DbContextOptions<ApplicationContext> option = CreateContextOptions(siteSetting.ConnectionStringSettings.ReadDatabaseConnectionString);
                return new ReadContext(option);
            });

            services.AddScoped((serviceProvider) =>
            {
                DbContextOptions<ApplicationContext> option = CreateContextOptions(siteSetting.ConnectionStringSettings.WriteDatabaseConnectionString);
                return new WriteContext(option);
            });

            DbContextOptions<ApplicationContext> CreateContextOptions(string connectionString)
            {
                DbContextOptions<ApplicationContext> contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                                     .UseSqlServer(connectionString)
                                     .UseLoggerFactory(_loggerFactory)
                                     .Options;

                return contextOptions;
            }

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(siteSetting.ConnectionStringSettings.WriteDatabaseConnectionString));
        }
        public static void AddMinimalMvc(this IServiceCollection services)
        {
            services.AddScoped<IResourceManager, ResourceManager<SharedResource>>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.Filters.Add(new AuthorizeFilter());
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization(options =>
              {
                  options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
              }).AddFluentValidation();
            services.AddSwaggerGenNewtonsoftSupport();
        }
        public static void AddElmahCore(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            if (!siteSetting.ApplicationSettings.ActivateElmah)
                return;

            services.AddElmah<SqlErrorLog>(options =>
                {
                    options.Path = siteSetting.ApplicationSettings.ElmahPath;
                    options.ConnectionString = siteSetting.ConnectionStringSettings.ElmahDatabaseConnectionString;
                    //options.CheckPermissionAction = httpContext => httpContext.User.Identity.IsAuthenticated;
                });

        }
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.Encryptkey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };

                IResourceManager resourceManager = null;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("Authentication failed.", context.Exception);
                        if (resourceManager == null)
                            resourceManager = context.HttpContext.RequestServices.GetRequiredService<IResourceManager>();
                        if (context.Exception != null)
                            throw new AppException(StatusCode.UnAuthorized, resourceManager.GetName(SharedResource.AuthenticateFailed), HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        if (resourceManager == null)
                            resourceManager = context.HttpContext.RequestServices.GetRequiredService<IResourceManager>();
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserCommandRepository>();
                        var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail(resourceManager.GetName(SharedResource.TokenNoClaims));

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail(resourceManager.GetName(SharedResource.TokenNoSecurityStamp));

                        //Find user and token from database and perform your custom validation
                        long userId = claimsIdentity.GetUserId<long>();
                        User user = userRepository.GetById(context.HttpContext.RequestAborted, userId, true);

                        //if (user.SecurityStamp != Guid.Parse(securityStamp))
                        //    context.Fail("Token security stamp is not valid.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail(resourceManager.GetName(SharedResource.TokenSecurityStampNotValid));

                        //if (!user.IsActive)
                        //    context.Fail("User is not active.");

                        userRepository.UpdateLastLoginDate(user);
                        unitOfWork.Commit();
                    },
                    OnChallenge = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                        if (resourceManager == null)
                            resourceManager = context.HttpContext.RequestServices.GetRequiredService<IResourceManager>();
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        if (context.AuthenticateFailure != null)
                            throw new AppException(StatusCode.UnAuthorized, resourceManager.GetName(SharedResource.AuthenticateFailure), HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(StatusCode.UnAuthorized, resourceManager.GetName(SharedResource.UnauthorizedToAccessResource), HttpStatusCode.Unauthorized);

                        //return Task.CompletedTask;
                    }
                };
            });
        }
        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                //url segment => {version}
                options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
                options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1
                options.ReportApiVersions = true;

                //ApiVersion.TryParse("1.0", out var version10);
                //ApiVersion.TryParse("1", out var version1);
                //var a = version10 == version1;

                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // api/posts?api-version=1

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // api/v1/posts

                //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
                // header => Api-Version : 1

                //options.ApiVersionReader = new MediaTypeApiVersionReader()

                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
                // combine of [querystring] & [urlsegment]
            });
        }
    }
}
