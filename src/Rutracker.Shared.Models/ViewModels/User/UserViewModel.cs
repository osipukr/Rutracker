using Rutracker.Shared.Models.ViewModels.User.Base;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}