using System;
using System.Collections.Generic;

namespace Rutracker.Server.Core.Entities
{
    public class Torrent : BaseEntity<long>
    {
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
        public string Hash { get; set; }
        public long TrackerId { get; set; }
        public long ForumId { get; set; }
        public string ForumTitle { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public int? DupConfidence { get; set; }
        public long? DupTorrentId { get; set; }
        public string DupTitle { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}