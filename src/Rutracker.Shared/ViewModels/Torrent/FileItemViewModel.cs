using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Shared.ViewModels.Torrent
{
    public class FileItemViewModel : BaseViewModel
    {
        public long Size { get; set; }
        public string Name { get; set; }
    }
}