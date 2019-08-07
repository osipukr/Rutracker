using System.Collections.Generic;

namespace Rutracker.Shared.ViewModels.Accounts.Response
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel()
        {
        }

        public LoginResponseViewModel(UserViewModel userViewModel, IEnumerable<string> roles)
        {
            User = userViewModel;
            Roles = roles;
        }

        public UserViewModel User { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}