using System;
using System.Runtime.Serialization;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Shared.Models.ViewModels.Comment
{
    [DataContract]
    public class CommentView : View<int>
    {
        [DataMember(Order = 1)]
        public string Text { get; set; }

        [DataMember(Order = 2)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 3)]
        public DateTime? ModifiedDate { get; set; }

        [DataMember(Order = 4)]
        public int LikesCount { get; set; }

        [DataMember(Order = 5)]
        public UserView User { get; set; }
    }
}