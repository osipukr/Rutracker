using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rutracker.Server.WebApi.Settings;

namespace Rutracker.Server.WebApi
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((context, configure) =>
                    {
                        configure.AddFile(context.Configuration.GetSection(nameof(FileLoggingSettings)).Bind);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}