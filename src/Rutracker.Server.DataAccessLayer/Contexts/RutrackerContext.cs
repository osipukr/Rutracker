using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Configurations;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class RutrackerContext : IdentityDbContext<User, Role, string>
    {
        public RutrackerContext(DbContextOptions<RutrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<Torrent> Torrents { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ForumConfiguration())
                   .ApplyConfiguration(new TorrentConfiguration())
                   .ApplyConfiguration(new FileConfiguration());
        }
    }
}