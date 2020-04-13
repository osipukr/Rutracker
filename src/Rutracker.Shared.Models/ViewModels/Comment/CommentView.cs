using System;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentView : View<int>
    {
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LikesCount { get; set; }
        public UserView User { get; set; }
    }
}