using NetEscapades.Extensions.Logging.RollingFile;

namespace Rutracker.Server.WebApi.Settings
{
    public class FileLoggingSettings
    {
        public string FileName { get; set; }
        public string LogDirectory { get; set; }
        public int? FileSizeLimit { get; set; }
        public string Extension { get; set; }
        public PeriodicityOptions Periodicity { get; set; }
    }
}