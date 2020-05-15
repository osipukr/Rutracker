using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Utils.IdentitySeed.Interfaces;

namespace Rutracker.Utils.IdentitySeed.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIdentitySeed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var provider = scope.ServiceProvider;

            foreach (var service in provider.GetServices<IContextSeed>())
            {
                service.SeedAsync().Wait();
            }

            return app;
        }
    }
}