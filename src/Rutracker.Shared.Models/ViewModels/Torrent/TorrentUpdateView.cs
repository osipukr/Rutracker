using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentUpdateView : View
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
    }
}