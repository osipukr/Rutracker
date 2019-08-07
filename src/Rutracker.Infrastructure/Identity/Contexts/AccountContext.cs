using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities.Accounts;

namespace Rutracker.Infrastructure.Identity.Contexts
{
    public class AccountContext : IdentityDbContext<User, Role, string>
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
    }
}