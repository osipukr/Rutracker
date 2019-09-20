using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangeEmailViewModel
    {
        [Required, EmailAddress] public string Email { get; set; }
    }
}