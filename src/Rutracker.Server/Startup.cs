using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace Rutracker.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDatabaseContext(_configuration)
                .AddCaching()
                .AddCustomResponseCompression()
                .AddAutoMapper(typeof(Startup))
                .AddMvc()
                .AddNewtonsoftJson()
                .Services
                .AddRepositories()
                .AddServices();

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperErrorPages()
                    .UseDebugging();
            }

            app.UseResponseCaching()
                .UseResponseCompression()
                .UseClientSideBlazorFiles<Client.Startup>()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
                })
                .SeedDatabase();
        }
    }
}