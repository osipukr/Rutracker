using System;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserProfileViewModel : UserViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}