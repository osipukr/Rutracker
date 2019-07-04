using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Services;
using Rutracker.Infrastructure.Data;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services;

namespace Rutracker.Server
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Configures caching for the application. Registers the <see cref="IMemoryCache" /> types with the services
        ///     collection or IoC container. Use the <see cref="IMemoryCache" /> otherwise.
        /// </summary>
        public static IServiceCollection AddCaching(this IServiceCollection services) =>
            services.AddMemoryCache();


        /// <summary>
        ///     Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(
            this IServiceCollection services) =>
            services
                .AddResponseCompression(
                    options =>
                    {
                        options.EnableForHttps = true;
                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                        {
                            MediaTypeNames.Application.Octet,
                            WasmMediaTypeNames.Application.Wasm,
                            MediaTypeNames.Application.Json,
                            MediaTypeNames.Image.Jpeg
                        });
                    })
                .Configure<GzipCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Optimal;
                });

        /// <summary>
        ///     Adds project repositories.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ITorrentRepository, TorrentRepository>();

        /// <summary>
        ///     Adds project services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddScoped<ITorrentService, TorrentService>()
                .AddScoped<ITorrentViewModelService, TorrentViewModelService>();

        /// <summary>
        ///     Adds project Database Context.
        /// </summary>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddDbContext<TorrentContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("TorrentConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
    }
}