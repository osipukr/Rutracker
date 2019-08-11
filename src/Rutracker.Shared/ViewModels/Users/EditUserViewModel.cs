using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels.Users
{
    public class EditUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}