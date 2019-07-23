using Rutracker.Client.Settings;

namespace Rutracker.Client
{
    public class ClientSettings
    {
        public ApiUriSettings ApiUriSettings { get; set; }
        public ViewSettings ViewSettings { get; set; }
        public MatToasterSettings MatToasterSettings { get; set; }
    }
}