using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rutracker.Shared.Models
{
    public class FileModel
    {
        [Required] public IFormFile File { get; set; }
        [Required] public string MimeType { get; set; }
    }
}