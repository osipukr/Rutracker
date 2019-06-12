namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentsIndexViewModel
    {
        public TorrentItemViewModel[] TorrentItems { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
    }
}