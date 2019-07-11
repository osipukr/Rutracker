using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Infrastructure.Data.Contexts.Configurations;

namespace Rutracker.Infrastructure.Data.Contexts
{
    public class TorrentContext : DbContext
    {
        public TorrentContext(DbContextOptions<TorrentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Torrent> Torrents { get; set; }
        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TorrentConfiguration());
            builder.ApplyConfiguration(new ForumConfiguration());
            builder.ApplyConfiguration(new FileConfiguration());
        }
    }
}