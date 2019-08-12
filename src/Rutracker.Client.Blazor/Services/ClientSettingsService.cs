using System.IO;
using Newtonsoft.Json;

namespace Rutracker.Client.Blazor.Services
{
    public static class ClientSettingsService
    {
        public static ClientSettings GetSettings(string filePath)
        {
            var assembly = typeof(Startup).Assembly;
            var resource = $"{assembly.GetName().Name}.{filePath}";

            using var stream = assembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);

            return JsonConvert.DeserializeObject<ClientSettings>(reader.ReadToEnd());
        }
    }
}