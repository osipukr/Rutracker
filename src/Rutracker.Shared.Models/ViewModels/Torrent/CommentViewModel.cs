using System;
using Rutracker.Shared.Models.ViewModels.Torrent.Base;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class CommentViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsModified { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int LikesCount { get; set; }
        public UserShortViewModel User { get; set; }
    }
}