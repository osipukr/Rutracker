using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentsIndexViewModel
    {
        public IEnumerable<TorrentItemViewModel> TorrentItems { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
    }
}  