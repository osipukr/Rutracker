using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ConfirmChangePhoneNumberViewModel
    {
        [Required] public string Phone { get; set; }
        [Required] public string Token { get; set; }
    }
}