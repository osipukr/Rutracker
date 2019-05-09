using Microsoft.EntityFrameworkCore;
using Rutracker.Server.Core.Entities;
using Rutracker.Server.Infrastructure.Data.Configurations;

namespace Rutracker.Server.Infrastructure.Data
{
    public class TorrentContext : DbContext
    {
        public TorrentContext(DbContextOptions<TorrentContext> options)
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