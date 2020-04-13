using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class PhoneConfirmationView
    {
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Token { get; set; }
    }
}