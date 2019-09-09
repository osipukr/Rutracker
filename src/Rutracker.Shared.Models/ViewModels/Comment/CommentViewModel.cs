using System;
using System.Collections.Generic;
using Rutracker.Shared.Models.ViewModels.Like;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    public class CommentViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsModified { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int LikesCount { get; set; }
        public IEnumerable<LikeViewModel> Likes { get; set; }
        public UserViewModel User { get; set; }
    }
}