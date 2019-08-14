using System;
using Rutracker.Shared.Models.ViewModels.Torrent.Base;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
    }
}