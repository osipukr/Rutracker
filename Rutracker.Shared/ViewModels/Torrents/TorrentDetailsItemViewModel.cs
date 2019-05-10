using System;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentDetailsItemViewModel : TorrentItemViewModel
    {
        public string Hash { get; set; }
        public string ForumTitle { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public FileItemViewModel[] TorrentFiles { get; set; }

        public TorrentDetailsItemViewModel()
        {
        }

        public TorrentDetailsItemViewModel(long id, DateTime date, long size, string title)
            : base(id, date, size, title)
        {
        }
    }
}