using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Like : Entity<int>
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}