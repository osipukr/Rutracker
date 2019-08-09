namespace Rutracker.Shared.ViewModels.Users
{
    public class UserResponseViewModel
    {
        public UserViewModel User { get; set; }
        public RoleViewModel[] Roles { get; set; }
    }
}