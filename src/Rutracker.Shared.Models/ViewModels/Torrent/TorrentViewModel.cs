using System;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentViewModel : TorrentShortViewModel
    {
        public DateTime CreatedAt { get; set; }
        public long Size { get; set; }
    }
}