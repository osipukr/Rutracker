using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User.Confirm
{
    public class ConfirmChangePhoneNumberViewModel
    {
        [Required] public string Phone { get; set; }
        [Required] public string Token { get; set; }
    }
}