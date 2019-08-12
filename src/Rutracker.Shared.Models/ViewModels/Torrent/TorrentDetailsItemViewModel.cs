using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsItemViewModel : TorrentItemViewModel
    {
        public string Hash { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public ForumItemViewModel Forum { get; set; }
        public FileItemViewModel[] Files { get; set; }
    }
}