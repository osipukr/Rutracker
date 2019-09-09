using Rutracker.Client.Blazor.Settings;

namespace Rutracker.Client.Blazor
{
    public class ClientSettings
    {
        public ApiUrlSettings ApiUrlSettings { get; set; }
        public ViewSettings ViewSettings { get; set; }
        public MatToasterSettings MatToasterSettings { get; set; }
    }
}