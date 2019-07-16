using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetEscapades.Extensions.Logging.RollingFile;

namespace Rutracker.Server
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureLogging(configure => configure.AddFile(options =>
                    {
                        options.FileName = "logs-";
                        options.LogDirectory = "Logs";
                        options.FileSizeLimit = 20 * 1024 * 1024;
                        options.Extension = "txt";
                        options.Periodicity = PeriodicityOptions.Daily;
                    }));
                    builder.UseStartup<Startup>();
                });
    }
}