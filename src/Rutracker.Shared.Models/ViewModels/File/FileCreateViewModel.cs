using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.File
{
    public class FileCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}