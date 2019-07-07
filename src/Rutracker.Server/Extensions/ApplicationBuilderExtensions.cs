using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;
using Rutracker.Server.Controllers;

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

        public static IApplicationBuilder UseCustomMvc(
            this IApplicationBuilder app) =>
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "api-pagination",
                        template: "api/torrents/pagination/{page}/{pageSize}",
                        defaults: new { controller = "Torrents", action = nameof(TorrentsController.Pagination) },
                        constraints: new { page = new IntRouteConstraint(), pageSize = new IntRouteConstraint() });

                    routes.MapRoute(
                        name: "api-get",
                        template: "api/torrents/{id}",
                        defaults: new { controller = "Torrents", action = nameof(TorrentsController.Get) },
                        constraints: new { id = new LongRouteConstraint() });

                    routes.MapRoute(
                        name: "api-titles",
                        template: "api/torrents/titles/{count}",
                        defaults: new { controller = "Torrents", action = nameof(TorrentsController.Titles) },
                        constraints: new { count = new IntRouteConstraint() });
                });

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