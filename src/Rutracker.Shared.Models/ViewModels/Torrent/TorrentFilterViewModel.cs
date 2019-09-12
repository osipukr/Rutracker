using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentFilterViewModel : FilterModel
    {
        [MaxLength(100)]
        public override string Search { get; set; }

        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
    }
}