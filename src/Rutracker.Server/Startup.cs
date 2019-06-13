using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server;
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
using Rutracker.Server.Services.Types;

namespace Rutracker.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
#pragma warning disable 618
            services.AddAutoMapper();
#pragma warning restore 618
            services.AddMvc().AddNewtonsoftJson();
            services.AddMemoryCache();
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Json,
                    WasmMediaTypeNames.Application.Wasm
                });
            });

            services.AddScoped<ITorrentRepository, TorrentRepository>();
            services.AddScoped<TorrentViewModelService>();
            services.AddScoped<CachedTorrentViewModelService>();
            services.AddTransient<Func<TorrentViewModelServiceEnum, ITorrentViewModelService>>(provider => key =>
            {
                switch (key)
                {
                    case TorrentViewModelServiceEnum.Default:
                        return provider.GetService<TorrentViewModelService>();

                    case TorrentViewModelServiceEnum.Cached:
                        return provider.GetService<CachedTorrentViewModelService>();

                    default: return null;
                }

            });

            services.AddDbContext<TorrentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TorrentConnection"));
                options.UseLazyLoadingProxies();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
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