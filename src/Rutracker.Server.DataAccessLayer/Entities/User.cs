using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsRegistrationFinished { get; set; }

        public virtual ICollection<Torrent> Torrents { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}