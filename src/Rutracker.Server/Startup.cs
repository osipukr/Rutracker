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

namespace Rutracker.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TorrentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TorrentConnection"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<ITorrentService, CachedTorrentViewModelService>();
            services.AddScoped<ITorrentRepository, TorrentRepository>();
            services.AddScoped<TorrentService>();

            services.AddMemoryCache();

            services.AddMvc().AddNewtonsoftJson();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
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