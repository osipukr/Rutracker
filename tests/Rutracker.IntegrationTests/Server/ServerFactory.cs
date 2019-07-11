using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Infrastructure.Extensions;
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

                    if (context.Database.EnsureCreated())
                    {
                        context.SeedAsync().Wait();
                    }
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}