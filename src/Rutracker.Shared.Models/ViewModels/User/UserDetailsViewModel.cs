namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailsViewModel : UserViewModel
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string ImageUrl { get; set; }
        public string[] Roles { get; set; }
    }
}