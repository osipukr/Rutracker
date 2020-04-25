using Microsoft.Extensions.DependencyInjection;
using Rutracker.Utils.IdentitySeed.Interfaces;
using Rutracker.Utils.IdentitySeed.Services;

namespace Rutracker.Utils.IdentitySeed.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentitySeed(this IServiceCollection services)
        {
            services.AddScoped<IContextSeed, RutrackerContextSeed>();

            return services;
        }
    }
}