using System;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentViewModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
    }
}