namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserDetailsViewModel : UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string[] Roles { get; set; }
    }
}