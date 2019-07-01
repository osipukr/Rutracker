using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.Server
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

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TorrentContext>();

                if (context.Database.EnsureCreated())
                {
                    context.SeedAsync().Wait();
                }
            }

            return app;
        }
    }
}