using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.WebApi;

namespace Rutracker.IntegrationTests.WebApi
{
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        private const string DatabaseName = nameof(WebApiFactory);

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<RutrackerContext>(options => options.UseInMemoryDatabase(DatabaseName));

                services.AddScoped<IContextSeed, WebApiContextSeed>();

                using var scope = services.BuildServiceProvider().CreateScope();
                var contextSeed = scope.ServiceProvider.GetRequiredService<IContextSeed>();

                contextSeed.SeedAsync().Wait();
            });
    }
}