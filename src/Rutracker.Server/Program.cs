using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rutracker.Server.Settings;

namespace Rutracker.Server
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureLogging((context, configure) =>
                    {
                        configure.AddFile(context.Configuration.GetSection(nameof(FileLoggingSettings)).Bind);
                    });
                    builder.UseStartup<Startup>();
                });
    }
}