using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangePhoneNumberViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(
            ErrorMessageResourceName = nameof(ChangePhoneNumberViewModelResource.PhoneNumberErrorMessage),
            ErrorMessageResourceType = typeof(ChangePhoneNumberViewModelResource))]
        [Display(
            Name = nameof(ChangePhoneNumberViewModelResource.PhoneNumberDisplayName),
            ResourceType = typeof(ChangePhoneNumberViewModelResource))]
        public string PhoneNumber { get; set; }
    }
}