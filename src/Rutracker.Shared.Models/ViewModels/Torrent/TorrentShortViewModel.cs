using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentShortViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int CommentsCount { get; set; }
    }
}