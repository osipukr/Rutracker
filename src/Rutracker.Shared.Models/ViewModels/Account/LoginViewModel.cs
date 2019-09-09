using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5,
            ErrorMessageResourceName = nameof(LoginViewModelResource.UserNameErrorMessage),
            ErrorMessageResourceType = typeof(LoginViewModelResource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(LoginViewModelResource.PasswordErrorMessage),
            ErrorMessageResourceType = typeof(LoginViewModelResource))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}