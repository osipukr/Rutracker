using System;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentItemViewModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
    }
}