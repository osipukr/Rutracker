using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class ChangeTorrentImageViewModel
    {
        [Required, Url] public string ImageUrl { get; set; }
    }
}