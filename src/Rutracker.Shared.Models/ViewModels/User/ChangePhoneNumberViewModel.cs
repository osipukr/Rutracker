using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangePhoneNumberViewModel
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}