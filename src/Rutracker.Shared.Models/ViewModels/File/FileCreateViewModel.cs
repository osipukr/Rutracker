using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rutracker.Shared.Models.ViewModels.File
{
    public class FileCreateViewModel
    {
        [Required] public IFormFile File { get; set; }
        [Required] public int TorrentId { get; set; }
    }
}