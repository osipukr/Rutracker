using Blazored.Toast;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Providers;
using Rutracker.Client.Host.Services;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models;
using Skclusive.Core.Component;
using Skclusive.Material.Layout;

namespace Rutracker.Client.Host
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredToast();
            services.AddProtectedBrowserStorage();
            services.AddHttpClient();

            services.Configure<ApiOptions>(_configuration.GetSection(nameof(ApiOptions)));
            services.Configure<ServerOptions>(_configuration.GetSection(nameof(ServerOptions)));

            services.AddHttpContextAccessor();
            services.AddScoped<IRenderContext>(sp =>
            {
                var httpContextAccessor = sp.GetService<IHttpContextAccessor>();
                var hasStarted = httpContextAccessor?.HttpContext?.Response.HasStarted;
                var isPreRendering = !(hasStarted.HasValue && hasStarted.Value);

                return new RenderContext(isServer: true, isPreRendering);
            });

            services.TryAddLayoutServices
            (
                new LayoutConfigBuilder()
                    .WithIsServer(true)
                    .WithIsPreRendering(true)
                    .WithResponsive(true)
                    .Build()
            );

            services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
            });

            services.AddScoped<HttpClientService>();
            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ApiAuthenticationStateProvider>());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<ITorrentService, TorrentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICommentService, CommentService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}