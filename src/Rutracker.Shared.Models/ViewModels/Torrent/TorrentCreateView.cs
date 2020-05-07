using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentCreateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int SubcategoryId { get; set; }
    }
}