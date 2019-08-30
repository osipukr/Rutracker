using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User.Change
{
    public class ChangeUserViewModel
    {
        [Display(Name = nameof(ChangeUserViewModelResource.FirstNameDisplayName), ResourceType = typeof(ChangeUserViewModelResource))]
        [StringLength(100,
            ErrorMessageResourceName = nameof(ChangeUserViewModelResource.FirstNameErrorMessage),
            ErrorMessageResourceType = typeof(ChangeUserViewModelResource))]
        public string FirstName { get; set; }

        [Display(Name = nameof(ChangeUserViewModelResource.LastNameDisplayName), ResourceType = typeof(ChangeUserViewModelResource))]
        [StringLength(100, 
            ErrorMessageResourceName = nameof(ChangeUserViewModelResource.LastNameErrorMessage),
            ErrorMessageResourceType = typeof(ChangeUserViewModelResource))]
        public string LastName { get; set; }
    }
}