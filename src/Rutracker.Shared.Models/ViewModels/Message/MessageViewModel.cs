using System;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Message
{
    public class MessageViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public UserViewModel User { get; set; }
    }
}