using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Client.BusinessLayer.Settings;

namespace Rutracker.Client.BlazorWasm
{
    public class ClientSettings
    {
        public ApiUrlOptions ApiUrlOptions { get; set; }
        public FileOptions FileOptions { get; set; }
        public HubSettings HubSettings { get; set; }
        public PageSettings PageSettings { get; set; }
        public ToasterSettings ToasterSettings { get; set; }
    }
}