namespace Rutracker.Shared.Models.ViewModels.User
{
    public class UserView : View<string>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
}