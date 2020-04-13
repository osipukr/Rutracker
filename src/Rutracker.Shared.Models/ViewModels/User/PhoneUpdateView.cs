using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class PhoneUpdateView
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}