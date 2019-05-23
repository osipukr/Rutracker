using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentsViewModel
    {
        public IEnumerable<TorrentItemViewModel> TorrentItems { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
        public FiltrationViewModel FiltrationModel { get; set; }
    }
}  