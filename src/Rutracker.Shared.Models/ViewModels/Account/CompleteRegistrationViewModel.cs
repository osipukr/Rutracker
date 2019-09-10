using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class CompleteRegistrationViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(CompleteRegistrationViewModelResource.PasswordErrorMessage),
            ErrorMessageResourceType = typeof(CompleteRegistrationViewModelResource))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),
            ErrorMessageResourceName = nameof(CompleteRegistrationViewModelResource.ConfirmPasswordErrorMessage),
            ErrorMessageResourceType = typeof(CompleteRegistrationViewModelResource))]
        public string ConfirmPassword { get; set; }
    }
}