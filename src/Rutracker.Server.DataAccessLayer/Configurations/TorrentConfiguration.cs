using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class TorrentConfiguration : IEntityTypeConfiguration<Torrent>
    {
        public void Configure(EntityTypeBuilder<Torrent> builder)
        {
            builder.ToTable("Torrents");

            builder.HasKey(torrent => torrent.Id);

            builder.Property(torrent => torrent.Id).ValueGeneratedOnAdd();
            builder.Property(torrent => torrent.Name).HasMaxLength(100).IsRequired();
            builder.Property(torrent => torrent.Description).HasMaxLength(200);
            builder.Property(torrent => torrent.Content).IsRequired();
            builder.Property(torrent => torrent.Size).IsRequired();
            builder.Property(torrent => torrent.Hash).HasMaxLength(250);
            builder.Property(torrent => torrent.TrackerId);
            builder.Property(torrent => torrent.AddedDate).IsRequired();
            builder.Property(torrent => torrent.ModifiedDate);
            builder.Property(torrent => torrent.SubcategoryId);

            builder.HasOne(torrent => torrent.Subcategory)
                .WithMany(subcategory => subcategory.Torrents)
                .HasForeignKey(torrent => torrent.SubcategoryId);

            builder.HasMany(torrent => torrent.Files)
                .WithOne(file => file.Torrent)
                .HasForeignKey(file => file.TorrentId);

            builder.HasMany(torrent => torrent.Comments)
                .WithOne(comment => comment.Torrent)
                .HasForeignKey(comment => comment.TorrentId);
        }
    }
}