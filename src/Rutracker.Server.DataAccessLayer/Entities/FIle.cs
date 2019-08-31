using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class File : BaseEntity<int>
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public int TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }
    }
}