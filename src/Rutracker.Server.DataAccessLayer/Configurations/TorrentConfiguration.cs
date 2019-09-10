using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class TorrentConfiguration : IEntityTypeConfiguration<Torrent>
    {
        public void Configure(EntityTypeBuilder<Torrent> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Size).IsRequired();
            builder.Property(t => t.SubcategoryId).IsRequired();

            builder.HasOne(t => t.Subcategory)
                .WithMany(s => s.Torrents)
                .HasForeignKey(t => t.SubcategoryId);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Torrents)
                .HasForeignKey(t => t.UserId);

            builder.HasMany(t => t.Files)
                .WithOne(f => f.Torrent)
                .HasForeignKey(f => f.TorrentId);

            builder.HasMany(t => t.Comments)
                .WithOne(c => c.Torrent)
                .HasForeignKey(c => c.TorrentId);
        }
    }
}