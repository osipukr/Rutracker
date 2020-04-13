using System;
using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Torrent : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public long Size { get; set; }
        public string Hash { get; set; }
        public int? TrackerId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int SubcategoryId { get; set; }

        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}