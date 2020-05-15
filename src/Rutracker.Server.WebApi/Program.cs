using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Rutracker.Server.WebApi
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.Configure<FormOptions>(options =>
                    {
                        options.ValueLengthLimit = int.MaxValue;
                        options.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
                    });
                })
                .ConfigureKestrel((context, options) =>
                {
                    options.AllowSynchronousIO = true;
                    options.Limits.MaxRequestBodySize = long.MaxValue;
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddApplicationInsights();
                })
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>();
    }
}