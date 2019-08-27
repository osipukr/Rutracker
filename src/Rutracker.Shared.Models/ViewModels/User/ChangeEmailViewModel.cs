using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress(
            ErrorMessageResourceName = nameof(ChangeEmailViewModelResource.EmailErrorMessage),
            ErrorMessageResourceType = typeof(ChangeEmailViewModelResource))]
        [Display(Name = nameof(ChangeEmailViewModelResource.EmailDislpayName), ResourceType = typeof(ChangeEmailViewModelResource))]
        public string Email { get; set; }
    }
}