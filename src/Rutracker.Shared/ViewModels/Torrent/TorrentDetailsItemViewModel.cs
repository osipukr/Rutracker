using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Shared.ViewModels.Torrent
{
    public class TorrentDetailsItemViewModel : TorrentItemViewModel
    {
        public string Hash { get; set; }
        public string ForumTitle { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public FileItemViewModel[] Files { get; set; }
    }
}