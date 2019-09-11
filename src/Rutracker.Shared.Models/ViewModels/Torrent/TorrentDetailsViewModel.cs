using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.File;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsViewModel : TorrentViewModel
    {
        public string Content { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
        public UserViewModel User { get; set; }
    }
}