using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Server;

namespace Rutracker.IntegrationTests.Server
{
    public class ServerFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices(services =>
                {
                    services
                        .AddEntityFrameworkInMemoryDatabase()
                        .AddDbContext<TorrentContext>((provider, options) =>
                        {
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                            options.UseInternalServiceProvider(provider);
                        }, ServiceLifetime.Singleton);

                    using var scope = services.BuildServiceProvider().CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<TorrentContext>();
                    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

                    if (context.Database.EnsureCreated())
                    {
                        TorrentContextSeed.SeedAsync(context, loggerFactory).Wait();
                    }
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}