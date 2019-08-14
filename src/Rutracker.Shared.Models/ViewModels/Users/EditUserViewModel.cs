using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.Users
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}