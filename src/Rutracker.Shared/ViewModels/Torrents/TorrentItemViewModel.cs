using System;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentItemViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
    }
}