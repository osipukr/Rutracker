using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Core.Services;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Infrastructure.Data.Repositories;
using Rutracker.Server.Filters;
using Rutracker.Server.Interfaces.Services;
using Rutracker.Server.Services;
using Rutracker.Server.Settings;

namespace Rutracker.Server.Extensions
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
        ///     Configures the settings by binding the contents of the appsettings.json file.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                // Adds IOptions<CacheSettings> to the services container.
                .Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));

        /// <summary>
        ///     Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddResponseCompression(
                    options =>
                    {
                        options.EnableForHttps = true;

                        var compressionSettings = configuration.GetSection(nameof(ResponseCompressionSettings))
                            .Get<ResponseCompressionSettings>();

                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(compressionSettings.MimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Optimal;
                });

        /// <summary>
        ///     Adds and configure Swagger middleware.
        /// </summary>
        public static IServiceCollection AddSwagger(
            this IServiceCollection services) =>
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Rutracker API",
                        Description = "The current version of the API"
                    });
                });

        /// <summary>
        ///     Adds custom mvc options.
        /// </summary>
        public static IMvcBuilder AddCustomMvcOptions(
            this IMvcBuilder builder) =>
            builder.AddMvcOptions(
                options =>
                {
                    options.Filters.Add<ControllerExceptionFilterAttribute>();
                    options.Filters.Add<ModelValidatorFilterAttribute>();

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
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