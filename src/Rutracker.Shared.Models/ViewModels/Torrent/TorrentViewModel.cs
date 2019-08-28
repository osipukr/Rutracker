using System;
using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentViewModel : BaseViewModel
    {
        public DateTime CreatedAt { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public int CommentsCount { get; set; }
    }
}