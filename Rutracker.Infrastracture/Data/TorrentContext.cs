using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Infrastructure.Data.Configurations;

namespace Rutracker.Infrastructure.Data
{
    public class TorrentContext : DbContext
    {
        public TorrentContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Torrent> Torrents { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TorrentConfiguration());
            builder.ApplyConfiguration(new FileConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}