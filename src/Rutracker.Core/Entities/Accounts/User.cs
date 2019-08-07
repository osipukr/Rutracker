using Microsoft.AspNetCore.Identity;

namespace Rutracker.Core.Entities.Accounts
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
}