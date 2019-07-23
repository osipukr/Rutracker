using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Services;

namespace Rutracker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var clientSettings = ClientSettingsService.GetSettings("clientsettings.json");

            services.AddSingleton(clientSettings.ApiUriSettings);
            services.AddSingleton(clientSettings.ViewSettings);
            services.AddSingleton<AppStateService>();
            services.AddMatToaster((MatToastConfiguration) clientSettings.MatToasterSettings);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;

            app.AddComponent<App>("app");
        }
    }
}