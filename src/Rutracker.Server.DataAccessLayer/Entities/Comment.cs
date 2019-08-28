using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Comment : BaseEntity<int>
    {
        public string Content { get; set; }
        public int TorrentId { get; set; }
        public string UserId { get; set; }

        public virtual Torrent Torrent { get; set; }
        public virtual User User { get; set; }
    }
}