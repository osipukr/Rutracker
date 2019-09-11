using System;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentsCount { get; set; }
    }
}