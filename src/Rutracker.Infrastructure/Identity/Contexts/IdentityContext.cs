using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<User, Role, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
    }
}