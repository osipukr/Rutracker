using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class ChangeTorrentImageFileViewModel
    {
        [Required] public IFormFile File { get; set; }
    }
}