using System.Linq;

namespace Rutracker.Core.Entities
{
    public class Forum : BaseEntity<long>
    {
        public string Title { get; set; }

        public virtual IQueryable<Torrent> Torrents { get; set; }
    }
}