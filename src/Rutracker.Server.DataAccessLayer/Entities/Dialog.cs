using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Dialog : BaseEntity<int>
    {
        public string Title { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserDialog> UserDialogs { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}