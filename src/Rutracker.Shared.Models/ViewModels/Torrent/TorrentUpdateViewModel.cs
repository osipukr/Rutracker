using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentUpdateViewModel
    {
        [Required, MaxLength(100)] public string Name { get; set; }
        [Required, MaxLength(300)] public string Description { get; set; }

        public string Content { get; set; }
    }
}