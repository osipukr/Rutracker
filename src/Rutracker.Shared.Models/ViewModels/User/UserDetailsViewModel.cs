using System.Collections.Generic;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailsViewModel : UserProfileViewModel
    {
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}