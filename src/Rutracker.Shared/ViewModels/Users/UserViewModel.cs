using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels.Users
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(IEnumerable<RoleViewModel> roles)
        {
            Roles = roles;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}