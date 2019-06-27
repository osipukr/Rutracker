using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.IntegrationTests.Server
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        // Error with IWebHostBuilder

        //protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        //    builder.ConfigureServices(services =>
        //    {
        //        services.AddEntityFrameworkInMemoryDatabase();

        //        var provider = services
        //            .AddEntityFrameworkInMemoryDatabase()
        //            .BuildServiceProvider();

        //        services.AddDbContext<TorrentContext>(options =>
        //        {
        //            options.UseInMemoryDatabase("InMemoryTestingDb");
        //            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //            options.UseInternalServiceProvider(provider);
        //        });

        //        using var scope = services.BuildServiceProvider().CreateScope();
        //        using var context = scope.ServiceProvider.GetRequiredService<TorrentContext>();

        //        if (context.Database.EnsureCreated())
        //        {
        //            context.SeedAsync().Wait();
        //        }
        //    });
    }
}