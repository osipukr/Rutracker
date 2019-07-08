using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
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
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
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
                .AddMvc()
                .AddCustomMvcOptions()
                .AddNewtonsoftJson()
                .Services
                .AddRepositories()
                .AddServices();

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app
                    .UseDeveloperErrorPages()
                    .UseDebugging();
            }

            app
                .UseResponseCaching()
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