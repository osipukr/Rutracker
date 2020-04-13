using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class EmailUpdateView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}