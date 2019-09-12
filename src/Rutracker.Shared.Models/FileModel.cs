using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Rutracker.Shared.Models
{
    public class FileModel
    {
        [Required] public Stream Stream { get; set; }
        [Required] public string MimeType { get; set; }
    }
}