using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.Message;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Dialog
{
    public class DialogViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public int UsersCount { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}