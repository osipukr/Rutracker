using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class File : BaseEntity<long>
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public long TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }
    }
}