using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class File : BaseEntity<int>
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Url { get; set; }
        public int TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }
    }
}