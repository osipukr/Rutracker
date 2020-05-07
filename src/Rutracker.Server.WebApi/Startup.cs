using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;
using Rutracker.Server.DataAccessLayer.Services;
using Rutracker.Server.WebApi.Filters;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Options;
using Rutracker.Server.WebApi.Services;
using Rutracker.Shared.Models;
using Rutracker.Utils.IdentitySeed.Extensions;
using FileOptions = Rutracker.Server.BusinessLayer.Options.FileOptions;

namespace Rutracker.Server.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = _configuration.GetConnectionString("ApplicationInsights");
            });

            services.AddDbContext<RutrackerContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(_configuration.GetConnectionString("SqlServer"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure();
                    });
            });

            services.AddMemoryCache();

            services.Configure<JwtOptions>(_configuration.GetSection("JwtOptions"));
            services.Configure<ClientOptions>(_configuration.GetSection("ClientOptions"));
            services.Configure<EmailAuthOptions>(_configuration.GetSection("EmailAuthOptions"));
            services.Configure<FileOptions>(_configuration.GetSection("FileOptions"));

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
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

            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
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
                var jwtSettings = _configuration.GetSection("JwtOptions").Get<JwtOptions>();

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
                options.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ValidatorFilter>();

                options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            })
            .AddNewtonsoftJson()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddIdentitySeed();

            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IUnitOfWork<RutrackerContext>, RutrackerUnitOfWork>();
            services.AddScoped<IDateService, DateService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<ITorrentService, TorrentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IStorageService, StorageService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Rutracker - API Endpoints";

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentitySeed();
        }
    }
}