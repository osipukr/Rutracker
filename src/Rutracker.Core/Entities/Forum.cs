using System.Collections.Generic;

namespace Rutracker.Core.Entities
{
    public class Forum : BaseEntity<long>
    {
        public string Title { get; set; }

        public virtual ICollection<Torrent> Torrents { get; set; }
    }
}