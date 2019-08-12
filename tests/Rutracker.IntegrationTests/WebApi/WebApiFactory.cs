using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi;

namespace Rutracker.IntegrationTests.WebApi
{
    public class ServerFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase()
                                           .BuildServiceProvider();

                    services
                        .AddDbContext<TorrentContext>(options => options
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .UseInternalServiceProvider(provider),
                            contextLifetime: ServiceLifetime.Singleton)
                        .AddDbContext<IdentityContext>(options => options
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .UseInternalServiceProvider(provider),
                            contextLifetime: ServiceLifetime.Singleton);

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var torrentContext = scope.ServiceProvider.GetRequiredService<TorrentContext>();
                    var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

                    if (torrentContext.Database.EnsureCreated())
                    {
                        TorrentContextSeed.SeedAsync(torrentContext, loggerFactory).Wait();
                    }

                    if (identityContext.Database.EnsureCreated())
                    {
                        var userManager = provider.GetService<UserManager<User>>();
                        var roleManager = provider.GetService<RoleManager<Role>>();

                        IdentityContextSeed.SeedAsync(identityContext, userManager, roleManager, loggerFactory).Wait();
                    }
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}