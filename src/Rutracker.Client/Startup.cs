using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Interfaces;
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
            services.AddSingleton<ITorrentsClientService, TorrentsClientService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}