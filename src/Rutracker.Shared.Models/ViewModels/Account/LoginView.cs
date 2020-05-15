using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class LoginView : View
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}