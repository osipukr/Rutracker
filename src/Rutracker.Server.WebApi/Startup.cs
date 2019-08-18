using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.Server.WebApi.Filters;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Services;
using Rutracker.Server.WebApi.Settings;

namespace Rutracker.Server.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDatabaseContext(services);

            services.AddMemoryCache();

            services.Configure<CacheSettings>(_configuration.GetSection(nameof(CacheSettings)));
            services.Configure<JwtSettings>(_configuration.GetSection(nameof(JwtSettings)));
            services.Configure<StorageSettings>(_configuration.GetSection(nameof(StorageSettings)));
            services.Configure<EmailSettings>(_configuration.GetSection(nameof(EmailSettings)));
            services.Configure<SmsSettings>(_configuration.GetSection(nameof(SmsSettings)));
            services.Configure<HostSettings>(_configuration.GetSection(nameof(HostSettings)));
            services.Configure<EmailConfirmationSettings>(_configuration.GetSection(nameof(EmailConfirmationSettings)));

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;

                var compressionSettings = _configuration.GetSection(nameof(ResponseCompressionSettings)).Get<ResponseCompressionSettings>();

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(compressionSettings.MimeTypes);
            })
            .Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddSwaggerGen(options =>
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

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<User, Role>(config =>
            {
                config.User.RequireUniqueEmail = true;

                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.ClaimsIssuer = jwtSettings.Issuer;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtSettings.SigningKey,

                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ControllerExceptionFilterAttribute>();
                options.Filters.Add<ModelValidatorFilterAttribute>();

                options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ISmsSender, SmsSender>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<ITorrentRepository, TorrentRepository>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITorrentService, TorrentService>();
            services.AddScoped<IAccountViewModelService, AccountViewModelService>();
            services.AddScoped<IUserViewModelService, UserViewModelService>();
            services.AddScoped<ITorrentViewModelService, TorrentViewModelService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            if (_environment.IsDevelopment())
            {
                app.UseBlazorDebugging();
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Rutracker - API Endpoints";
                options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
            });

            app.UseClientSideBlazorFiles<Client.Blazor.Startup>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToClientSideBlazor<Client.Blazor.Startup>(filePath: "index.html");
            });

            SeedDatabase(app);
        }

        private void AddDatabaseContext(IServiceCollection services)
        {
            services.AddDbContext<TorrentContext>(options => options
                    .UseSqlServer(
                        _configuration.GetConnectionString("TorrentConnection"),
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLazyLoadingProxies());

            services.AddDbContext<IdentityContext>(options => options
                    .UseSqlServer(
                        _configuration.GetConnectionString("IdentityConnection"),
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
                    .UseLazyLoadingProxies());
        }

        private void SeedDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var provider = scope.ServiceProvider;
            var torrentContext = provider.GetRequiredService<TorrentContext>();
            var identityContext = provider.GetRequiredService<IdentityContext>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            if (torrentContext.Database.EnsureCreated())
            {
                TorrentContextSeed.SeedAsync(torrentContext, loggerFactory).Wait();
            }

            if (identityContext.Database.EnsureCreated())
            {
                var userManager = provider.GetService<UserManager<User>>();
                var roleManager = provider.GetService<RoleManager<Role>>();

                IdentityContextSeed.SeedAsync(identityContext, userManager, roleManager, loggerFactory).Wait();
            }
        }
    }
}