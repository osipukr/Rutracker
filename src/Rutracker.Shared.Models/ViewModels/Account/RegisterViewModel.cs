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
    }
}