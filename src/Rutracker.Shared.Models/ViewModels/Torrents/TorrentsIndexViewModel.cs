using Rutracker.Shared.Models.ViewModels.Shared;

namespace Rutracker.Shared.Models.ViewModels.Torrents
{
    public class TorrentsIndexViewModel
    {
        public TorrentItemViewModel[] TorrentItems { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
    }
}