using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ForgotPasswordView
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string UserName { get; set; }
    }
}