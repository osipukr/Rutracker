using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}