using System;
using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsViewModel : BaseViewModel
    {
        public DateTime CreatedAt { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public FileViewModel[] Files { get; set; }
        public CommentViewModel[] Comments { get; set; }
    }
}