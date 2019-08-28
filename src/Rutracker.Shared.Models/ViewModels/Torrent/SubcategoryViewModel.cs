using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class SubcategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public TorrentViewModel[] Torrents { get; set; }
    }
}