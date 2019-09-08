using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.Role;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailsViewModel : UserProfileViewModel
    {
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}