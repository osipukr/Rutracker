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
        private readonly IConfigurationRoot _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment environment)
        {
            _environment = environment;

            _configuration = new ConfigurationBuilder()
               .SetBasePath(_environment.ContentRootPath)
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDatabaseContext(_configuration)
                .AddCaching()
                .AddCustomOptions(_configuration)
                .AddCustomResponseCompression(_configuration)
                .AddAutoMapper(typeof(Startup))
                .AddControllersWithMvcOptions()
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
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
                })
                .SeedDatabase();
        }
    }
}