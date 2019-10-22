using System;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Message : BaseEntity<int>
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public int DialogId { get; set; }
        public string UserId { get; set; }

        public virtual Dialog Dialog { get; set; }
        public virtual User User { get; set; }
    }
}