using Microsoft.AspNetCore.Identity;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Role : IdentityRole
    {
        public bool IsStockRole { get; set; }
    }
}