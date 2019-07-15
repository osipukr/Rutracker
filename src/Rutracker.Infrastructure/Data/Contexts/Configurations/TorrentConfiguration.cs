using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Core.Entities;

namespace Rutracker.Infrastructure.Data.Contexts.Configurations
{
    public class TorrentConfiguration : IEntityTypeConfiguration<Torrent>
    {
        public void Configure(EntityTypeBuilder<Torrent> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedNever().IsRequired();

            builder.HasOne(t => t.Forum)
                .WithMany(f => f.Torrents)
                .HasForeignKey(t => t.ForumId);

            builder.HasMany(t => t.Files)
                .WithOne(f => f.Torrent)
                .HasForeignKey(f => f.TorrentId);
        }
    }
}