using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentDetailsItemViewModel : TorrentItemViewModel
    {
        public string Hash { get; set; }
        public string ForumTitle { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public IEnumerable<FileItemViewModel> Files { get; set; }
    }
}