using System;
using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsViewModel : BaseViewModel
    {
        public DateTime CreatedAt { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Content { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
        public UserViewModel User { get; set; }
    }
}