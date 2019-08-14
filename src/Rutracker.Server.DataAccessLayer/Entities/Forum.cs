using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Forum : BaseEntity<long>
    {
        public string Title { get; set; }

        public virtual ICollection<Torrent> Torrents { get; set; }
    }
}