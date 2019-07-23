using MatBlazor;

namespace Rutracker.Client.Settings
{
    public class MatToasterSettings
    {
        public MatToastPosition Position { get; set; }
        public bool PreventDuplicates { get; set; }
        public bool NewestOnTop { get; set; }
        public bool ShowProgressBar { get; set; }
        public bool ShowCloseButton { get; set; }
        public int MaximumOpacity { get; set; }
        public int VisibleStateDuration { get; set; }

        public static explicit operator MatToastConfiguration(MatToasterSettings settings) =>
            new MatToastConfiguration
            {
                Position = settings.Position,
                PreventDuplicates = settings.PreventDuplicates,
                NewestOnTop = settings.NewestOnTop,
                ShowProgressBar = settings.ShowProgressBar,
                ShowCloseButton = settings.ShowCloseButton,
                MaximumOpacity = settings.MaximumOpacity,
                VisibleStateDuration = settings.VisibleStateDuration
            };
    }
}
