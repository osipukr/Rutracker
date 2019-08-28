using System;
using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Torrent : BaseEntity<int>
    {
        public DateTime RegisteredAt { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Content { get; set; }
        public int SubcategoryId { get; set; }
        public string UserId { get; set; }

        public virtual Subcategory Subcategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}