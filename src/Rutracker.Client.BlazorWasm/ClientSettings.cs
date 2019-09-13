using Rutracker.Client.BlazorWasm.Settings;

namespace Rutracker.Client.BlazorWasm
{
    public class ClientSettings
    {
        public ApiUrlSettings ApiUrlSettings { get; set; }
        public ViewSettings ViewSettings { get; set; }
        public MatToasterSettings MatToasterSettings { get; set; }
    }
}