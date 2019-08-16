using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = nameof(LoginViewModelResource.UserNameDisplayName), ResourceType = typeof(LoginViewModelResource))]
        [StringLength(maximumLength: 100, MinimumLength = 5,
            ErrorMessageResourceName = nameof(LoginViewModelResource.UserNameErrorMessage),
            ErrorMessageResourceType = typeof(LoginViewModelResource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(LoginViewModelResource.PasswordDisplayName), ResourceType = typeof(LoginViewModelResource))]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(LoginViewModelResource.PasswordErrorMessage),
            ErrorMessageResourceType = typeof(LoginViewModelResource))]
        public string Password { get; set; }

        [Display(Name = nameof(LoginViewModelResource.RememberMeDisplayName), ResourceType = typeof(LoginViewModelResource))]
        public bool RememberMe { get; set; }
    }
}