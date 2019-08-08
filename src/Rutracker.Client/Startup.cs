using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Services;
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
            services.AddMatToaster((MatToastConfiguration) clientSettings.MatToasterSettings);

            services.AddAuthorizationCore();
            services.AddSingleton<HttpClientService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<AuthenticationService>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthenticationService>());
            services.AddSingleton<AppState>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;

            app.AddComponent<App>("app");
        }
    }
}