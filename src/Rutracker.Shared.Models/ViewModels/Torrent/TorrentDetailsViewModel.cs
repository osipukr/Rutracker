using System;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsViewModel : TorrentViewModel
    {
        public string Content { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public UserViewModel User { get; set; }
    }
}