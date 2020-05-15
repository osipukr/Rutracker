using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Account
{
    public class ForgotPasswordView : View
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string UserName { get; set; }
    }
}