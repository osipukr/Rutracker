using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rutracker.Core.Entities.Identity;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Infrastructure.Identity.Contexts;

namespace Rutracker.Server.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Configure tools used to help with debugging the application.
        /// </summary>
        public static IApplicationBuilder UseDebugging(this IApplicationBuilder app)
        {
            app.UseBlazorDebugging();

            return app;
        }

        /// <summary>
        ///     Adds developer friendly error pages for the application which contain extra debug and exception information.
        ///     Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder app) =>
            app.UseDeveloperExceptionPage();

        /// <summary>
        ///     Filling the database with initial values ​​if it is empty.
        /// </summary>
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var provider = scope.ServiceProvider;
                var torrentContext = provider.GetRequiredService<TorrentContext>();
                var identityContext = provider.GetRequiredService<IdentityContext>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

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
            }

            return app;
        }
    }
}