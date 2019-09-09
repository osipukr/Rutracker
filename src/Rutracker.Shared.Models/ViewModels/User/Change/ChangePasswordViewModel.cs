using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User.Change
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(ChangePasswordViewModelResource.OldPasswordErrorMessage),
            ErrorMessageResourceType = typeof(ChangePasswordViewModelResource))]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 6,
            ErrorMessageResourceName = nameof(ChangePasswordViewModelResource.NewPasswordErrorMessage),
            ErrorMessageResourceType = typeof(ChangePasswordViewModelResource))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), 
            ErrorMessageResourceName = nameof(ChangePasswordViewModelResource.ConfirmNewPasswordErrorMessage),
            ErrorMessageResourceType = typeof(ChangePasswordViewModelResource))]
        public string ConfirmNewPassword { get; set; }
    }
}