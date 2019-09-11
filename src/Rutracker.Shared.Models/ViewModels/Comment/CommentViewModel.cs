using System;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int LikesCount { get; set; }
        public UserViewModel User { get; set; }
    }
}