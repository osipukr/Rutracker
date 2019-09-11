namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentFilterViewModel : FilterViewModel
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
    }
}