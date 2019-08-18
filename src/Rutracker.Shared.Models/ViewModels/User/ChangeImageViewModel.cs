using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ChangeImageViewModel
    {
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public byte[] ImageBytes { get; set; }

        public string FileType{ get; set; }
    }
}