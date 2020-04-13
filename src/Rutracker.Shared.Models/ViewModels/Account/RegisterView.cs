using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class RegisterView
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}