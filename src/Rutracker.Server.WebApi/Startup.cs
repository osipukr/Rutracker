using System;
using AutoMapper;
using Boilerplate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rutracker.Server.WebApi.Extensions;

namespace Rutracker.Server.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDatabaseContext(_configuration)
                .AddCaching()
                .AddCustomOptions(_configuration)
                .AddCustomResponseCompression(_configuration)
                .AddSwagger()
                .AddAutoMapper(typeof(Startup))
                .AddCustomIdentity(_configuration)
                .AddControllers()
                .AddCustomMvcOptions()
                .Services
                .AddAuthorization()
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
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "Rutracker - API Endpoints";
                    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
                })
                .UseClientSideBlazorFiles<Client.Blazor.Startup>()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToClientSideBlazor<Client.Blazor.Startup>(filePath: "index.html");
                })
                .SeedDatabase();
        }
    }
}