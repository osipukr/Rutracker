using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rutracker.Core.Entities.Identity;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Core.Services;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Infrastructure.Data.Repositories;
using Rutracker.Infrastructure.Identity.Contexts;
using Rutracker.Server.Filters;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services;
using Rutracker.Server.Settings;

namespace Rutracker.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Configures caching for the application. Registers the <see cref="IMemoryCache" /> types with the services
        ///     collection or IoC container. Use the <see cref="IMemoryCache" /> otherwise.
        /// </summary>
        public static IServiceCollection AddCaching(this IServiceCollection services) =>
            services.AddMemoryCache();

        /// <summary>
        ///     Configures the settings by binding the contents of the appsettings.json file.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services, IConfiguration configuration) =>
            services
                .Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)))
                .Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        /// <summary>
        ///     Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddResponseCompression(
                    options =>
                    {
                        options.EnableForHttps = true;

                        var compressionSettings = configuration.GetSection(nameof(ResponseCompressionSettings))
                            .Get<ResponseCompressionSettings>();

                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(compressionSettings.MimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Optimal;
                });

        /// <summary>
        ///     Adds custom identity with Authentication and JwtBearer.
        /// </summary>
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddIdentity<User, Role>(config =>
                {
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireDigit = false;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders()
                .Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtSetting = configuration.GetSection(nameof(JwtSettings));

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.ClaimsIssuer = jwtSetting[nameof(JwtSettings.Issuer)];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSetting[nameof(JwtSettings.Issuer)],

                        ValidateAudience = true,
                        ValidAudience = jwtSetting[nameof(JwtSettings.Audience)],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtSettings.SigningKey,

                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                })
                .Services;

        /// <summary>
        ///     Adds and configure Swagger middleware.
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services
                .AddSwaggerGen(options =>
                {
                    // Add the XML comment file for this assembly, so it's contents can be displayed.
                    var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var filePath = Path.Combine(AppContext.BaseDirectory, file);
                    options.IncludeXmlComments(filePath, includeControllerXmlComments: true);

                    options.SwaggerDoc(name: "v1", new OpenApiInfo
                    {
                        Title = "Rutracker API",
                        Description = "The current version of the API",
                        Version = "v1"
                    });
                });

        /// <summary>
        ///     Adds custom mvc options.
        /// </summary>
        public static IMvcBuilder AddCustomMvcOptions(this IMvcBuilder builder) =>
            builder.AddMvcOptions(options =>
                {
                    options.Filters.Add<ControllerExceptionFilterAttribute>();
                    options.Filters.Add<ModelValidatorFilterAttribute>();

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

        /// <summary>
        ///     Adds project repositories.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ITorrentRepository, TorrentRepository>();

        /// <summary>
        ///     Adds project services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddSingleton<IJwtFactory, JwtFactory>()
                .AddScoped<ITorrentService, TorrentService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAccountViewModelService, AccountViewModelService>()
                .AddScoped<IUserViewModelService, UserViewModelService>()
                .AddScoped<ITorrentViewModelService, TorrentViewModelService>();

        /// <summary>
        ///     Adds project Database Context.
        /// </summary>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment) =>
            services
                .AddDbContext<TorrentContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("TorrentConnection"),
                        sqlServerOptions =>
                        {
                            sqlServerOptions.EnableRetryOnFailure();
                        })
                    .EnableSensitiveDataLogging(environment.IsDevelopment())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddDbContext<IdentityContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("IdentityConnection"),
                        sqlServerOptions =>
                        {
                            sqlServerOptions.EnableRetryOnFailure();
                        })
                    .EnableSensitiveDataLogging(environment.IsDevelopment())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    }
}