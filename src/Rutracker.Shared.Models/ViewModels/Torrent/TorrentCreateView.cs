using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentCreateView : View
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long Size { get; set; }

        [Required]
        public string Hash { get; set; }

        public int? TrackerId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int SubcategoryId { get; set; }
    }
}