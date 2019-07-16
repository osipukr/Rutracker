using MatBlazor;

namespace Rutracker.Client.Settings
{
    public class MatToasterSettings
    {
        public MatToastPosition Position { get; set; }
        public bool PreventDuplicates { get; set; }
        public bool NewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public int MaximumOpacity { get; set; }
        public int VisibleStateDuration { get; set; }
    }
}
