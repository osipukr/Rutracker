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
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder(null)
                .ConfigureServices(services =>
                {
                    services
                        .AddEntityFrameworkProxies()
                        .AddEntityFrameworkInMemoryDatabase()
                        .AddDbContext<RutrackerContext>((provider, options) => options
                            .UseInternalServiceProvider(provider)
                            .UseLazyLoadingProxies()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString()),
                            contextLifetime: ServiceLifetime.Singleton);

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var context = scope.ServiceProvider.GetRequiredService<RutrackerContext>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
                    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

                    if (context.Database.EnsureCreated())
                    {
                        RutrackerContextSeed.SeedAsync(context, userManager, roleManager, loggerFactory).Wait();
                    }
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}