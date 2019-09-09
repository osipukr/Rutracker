using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User.Change
{
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress(
            ErrorMessageResourceName = nameof(ChangeEmailViewModelResource.EmailErrorMessage),
            ErrorMessageResourceType = typeof(ChangeEmailViewModelResource))]
        public string Email { get; set; }
    }
}