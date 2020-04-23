using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.Role;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailView : UserView
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<RoleView> Roles { get; set; }
    }
}