using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rutracker.Core.Interfaces;
using Rutracker.Infrastructure.Data;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Components.Server;
using Rutracker.Server.Services.Types;

namespace Rutracker.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().AddNewtonsoftJson();
            services.AddMemoryCache();
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                    MediaTypeNames.Application.Json
                });
            });

            services.AddScoped<ITorrentRepository, TorrentRepository>();
            services.AddScoped<TorrentViewModelService>();
            services.AddScoped<CachedTorrentViewModelService>();
            services.AddTransient<Func<TorrentViewModelServiceType, ITorrentViewModelService>>(provider => types =>
            {
                switch (types)
                {
                    case TorrentViewModelServiceType.Default:
                        return provider.GetService<TorrentViewModelService>();

                    case TorrentViewModelServiceType.Cached:
                        return provider.GetService<CachedTorrentViewModelService>();

                    default: return null;
                }

            });

            services.AddDbContext<TorrentContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(Configuration.GetConnectionString("TorrentConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}