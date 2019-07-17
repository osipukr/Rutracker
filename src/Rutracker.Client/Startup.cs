using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Client.Interfaces;
using Rutracker.Client.Services;
using Rutracker.Client.Settings;

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

            AddMatToaster(services, clientSettings.MatToasterSettings);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;

            app.AddComponent<App>("app");
        }

        private static void AddMatToaster(IServiceCollection services, MatToasterSettings settings) =>
            services.AddMatToaster(config =>
            {
                config.Position = settings.Position;
                config.PreventDuplicates = settings.PreventDuplicates;
                config.NewestOnTop = settings.NewestOnTop;
                config.ShowProgressBar = settings.ShowProgressBar;
                config.ShowCloseButton = settings.ShowCloseButton;
                config.MaximumOpacity = settings.MaximumOpacity;
                config.VisibleStateDuration = settings.VisibleStateDuration;
            });
    }
}