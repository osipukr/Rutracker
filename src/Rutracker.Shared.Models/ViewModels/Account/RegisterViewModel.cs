using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}