using MatBlazor;
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
            services.AddMatToaster((MatToastConfiguration) clientSettings.MatToasterSettings);

            services.AddSingleton<HttpClientService>();
            services.AddSingleton<AppState>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}