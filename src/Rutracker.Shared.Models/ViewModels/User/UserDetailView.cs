using System.Collections.Generic;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailView : UserView
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}