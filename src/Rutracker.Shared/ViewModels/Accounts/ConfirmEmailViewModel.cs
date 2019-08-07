using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels.Accounts
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}