using System;
using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Subcategory : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Torrent> Torrents { get; set; }
    }
}