using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class EditUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public byte[] ImageBytes { get; set; }
    }
}