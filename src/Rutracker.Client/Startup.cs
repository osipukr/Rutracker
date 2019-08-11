using Blazored.LocalStorage;
using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Services;
using Rutracker.Client.Services.Implementations;
using Rutracker.Client.Services.Interfaces;

namespace Rutracker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var clientSettings = ClientSettingsService.GetSettings("clientsettings.json");

            services.AddSingleton(clientSettings.ApiUriSettings);
            services.AddSingleton(clientSettings.ViewSettings);

            services.AddBlazoredModal();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddSingleton<HttpClientService>();
            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ApiAuthenticationStateProvider>());
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITorrentService, TorrentService>();
            services.AddSingleton<AppState>();

            services.AddMatToaster((MatToastConfiguration)clientSettings.MatToasterSettings);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}