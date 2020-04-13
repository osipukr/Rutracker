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

            builder.Property(torrent => torrent.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(torrent => torrent.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(torrent => torrent.Description).HasColumnName("Description").HasMaxLength(200);
            builder.Property(torrent => torrent.Content).HasColumnName("Content");
            builder.Property(torrent => torrent.Size).HasColumnName("Size").IsRequired();
            builder.Property(torrent => torrent.Hash).HasColumnName("Hash").HasMaxLength(250);
            builder.Property(torrent => torrent.TrackerId).HasColumnName("TrackerId");
            builder.Property(torrent => torrent.AddedDate).HasColumnName("AddedDate").HasColumnType("datetime");
            builder.Property(torrent => torrent.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime");
            builder.Property(torrent => torrent.SubcategoryId).HasColumnName("SubcategoryId");

            builder.HasOne(torrent => torrent.Subcategory)
                .WithMany(subcategory => subcategory.Torrents)
                .HasForeignKey(torrent => torrent.SubcategoryId);
        }
    }
}