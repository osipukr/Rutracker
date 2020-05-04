using System;
using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Comment : Entity<int>
    {
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int TorrentId { get; set; }
        public string UserId { get; set; }

        public virtual Torrent Torrent { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}