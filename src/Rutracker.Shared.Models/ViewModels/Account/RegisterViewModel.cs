using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5,
            ErrorMessageResourceName = nameof(RegisterViewModelResource.UserNameErrorMessage),
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(RegisterViewModelResource.EmailErrorMessage),
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(RegisterViewModelResource.PasswordErrorMessage),
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),
            ErrorMessageResourceName = nameof(RegisterViewModelResource.ConfirmPasswordErrorMessage),
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]
        public string ConfirmPassword { get; set; }
    }
}