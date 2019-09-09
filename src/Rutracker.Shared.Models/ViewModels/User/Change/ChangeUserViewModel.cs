using System.ComponentModel.DataAnnotations;
using Rutracker.Shared.Models.Resources;

namespace Rutracker.Shared.Models.ViewModels.User.Change
{
    public class ChangeUserViewModel
    {
        [StringLength(100,
            ErrorMessageResourceName = nameof(ChangeUserViewModelResource.FirstNameErrorMessage),
            ErrorMessageResourceType = typeof(ChangeUserViewModelResource))]
        public string FirstName { get; set; }

        [StringLength(100, 
            ErrorMessageResourceName = nameof(ChangeUserViewModelResource.LastNameErrorMessage),
            ErrorMessageResourceType = typeof(ChangeUserViewModelResource))]
        public string LastName { get; set; }
    }
}