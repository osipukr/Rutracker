namespace Rutracker.Shared.Models.ViewModels.User.Change
{
    public class ChangeImageViewModel
    {
        public string ImageUrl { get; set; }
        public byte[] ImageBytes { get; set; }
        public string FileType{ get; set; }
        public bool IsDelete { get; set; }
    }
}