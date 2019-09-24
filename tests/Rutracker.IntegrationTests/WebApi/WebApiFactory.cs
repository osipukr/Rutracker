using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.WebApi;

namespace Rutracker.IntegrationTests.WebApi
{
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<RutrackerContext>(options => options.UseInMemoryDatabase(nameof(WebApiFactory)));

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<RutrackerContext>();

                if (context.Database.EnsureCreated())
                {
                    new WebApiContextSeed(context).SeedAsync().Wait();
                }
            });
    }
}