using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentStockCreateView : View
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(250)]
        public string Hash { get; set; }

        [Required]
        public int? TrackerId { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long Size { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int SubcategoryId { get; set; }
    }
}