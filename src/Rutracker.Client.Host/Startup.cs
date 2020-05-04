using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rutracker.Client.Host.Helpers;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Providers;
using Rutracker.Client.Host.Services;
using Rutracker.Client.Infrastructure.Extensions;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Client.View.Helpers;
using Rutracker.Shared.Models;
using Skclusive.Core.Component;
using Skclusive.Material.Layout;
using Toolbelt.Blazor.Extensions.DependencyInjection;

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
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = _configuration.GetConnectionString("ApplicationInsights");
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddProtectedBrowserStorage();

            services.Configure<ApiOptions>(_configuration.GetSection("ApiOptions"));
            services.Configure<ServerOptions>(_configuration.GetSection("ServerOptions"));

            services.AddHttpContextAccessor();
            services.AddScoped<IRenderContext>(provider =>
            {
                var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
                var isPreRending = !httpContextAccessor.IsServerStarted();

                return new RenderContext(true, isPreRending);
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

            services.AddBlazoredModal();
            services.AddHeadElementHelper();
            services.AddBlazorContextMenu();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.ShowProgressBar = false;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 5000;
            });

            services.AddScoped<PathHelper>();
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
            // Workaround for https://github.com/aspnet/AspNetCore/issues/13470
            app.Use((context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;

                return next.Invoke();
            });

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
            app.UseHeadElementServerPrerendering();
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