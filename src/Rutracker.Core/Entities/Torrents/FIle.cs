namespace Rutracker.Core.Entities.Torrents
{
    public class File : BaseEntity<long>
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public long TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }
    }
}