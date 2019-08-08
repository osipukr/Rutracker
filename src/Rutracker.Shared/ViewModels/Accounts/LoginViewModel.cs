using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}