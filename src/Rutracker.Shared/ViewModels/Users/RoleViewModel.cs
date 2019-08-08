namespace Rutracker.Shared.ViewModels.Users
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public RoleViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}