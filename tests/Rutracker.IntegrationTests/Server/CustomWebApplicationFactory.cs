using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;
using Rutracker.Server;

namespace Rutracker.IntegrationTests.Server
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                var provider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<TorrentContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestingDb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<TorrentContext>();

                if (context.Database.EnsureCreated())
                {
                    context.SeedAsync().Wait();
                }
            });
    }
}