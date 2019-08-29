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
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RutrackerContext>(options => options
                .UseSqlServer(
                    _configuration.GetConnectionString("RutrackerConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
                .UseLazyLoadingProxies());

            services.AddMemoryCache();

            services.Configure<CacheSettings>(_configuration.GetSection(nameof(CacheSettings)));
            services.Configure<JwtSettings>(_configuration.GetSection(nameof(JwtSettings)));
            services.Configure<StorageSettings>(_configuration.GetSection(nameof(StorageSettings)));
            services.Configure<EmailSettings>(_configuration.GetSection(nameof(EmailSettings)));
            services.Configure<SmsSettings>(_configuration.GetSection(nameof(SmsSettings)));
            services.Configure<HostSettings>(_configuration.GetSection(nameof(HostSettings)));
            services.Configure<EmailConfirmationSettings>(_configuration.GetSection(nameof(EmailConfirmationSettings)));
            services.Configure<EmailChangeConfirmationSettings>(_configuration.GetSection(nameof(EmailChangeConfirmationSettings)));

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
                config.SignIn.RequireConfirmedEmail = true;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                config.User.RequireUniqueEmail = true;

                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<RutrackerContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
            });

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
            services.AddSingleton<ISmsService, SmsService>();
            services.AddScoped<ITorrentRepository, TorrentRepository>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITorrentService, TorrentService>();
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

            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var provider = scope.ServiceProvider;
            var context = provider.GetRequiredService<RutrackerContext>();
            var userManager = provider.GetService<UserManager<User>>();
            var roleManager = provider.GetService<RoleManager<Role>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            if (context.Database.EnsureCreated())
            {
                RutrackerContextSeed.SeedAsync(context, userManager, roleManager, loggerFactory).Wait();
            }
        }
    }
}