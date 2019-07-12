using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Boilerplate.AspNetCore;
using Rutracker.Server.Extensions;

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
                .AddCustomOptions(_configuration)
                .AddCustomResponseCompression(_configuration)
                .AddAutoMapper(typeof(Startup))
                .AddControllers()
                .AddCustomMvcOptions()
                .Services
                .AddRepositories()
                .AddServices();

        public void Configure(IApplicationBuilder application)
        {
            application
                .UseResponseCaching()
                .UseResponseCompression()
                .UseIf(
                    _environment.IsDevelopment(),
                    x => x
                        .UseDeveloperErrorPages()
                        .UseDebugging())
                .UseClientSideBlazorFiles<Client.Startup>()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
                })
                .SeedDatabase();
        }
    }
}