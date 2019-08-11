using Microsoft.AspNetCore.Identity;

namespace Rutracker.Core.Entities.Identity
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}