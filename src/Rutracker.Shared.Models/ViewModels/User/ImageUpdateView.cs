using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ImageUpdateView
    {
        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}