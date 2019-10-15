namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class UserDialog
    {
        public string UserId { get; set; }
        public int DialogId { get; set; }

        public virtual User User { get; set; }
        public virtual Dialog Dialog { get; set; }
    }
}