using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
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
            services.AddSingleton<ITorrentsClientService, TorrentsClientService>();
            services.AddScoped(options => new HttpClient
            {
                BaseAddress = new Uri(options.GetRequiredService<IUriHelper>().GetBaseUri())
            });
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}