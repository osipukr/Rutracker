using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class TorrentConfiguration : IEntityTypeConfiguration<Torrent>
    {
        private const string TORRENT_TABLE_NAME = "Torrents";

        public void Configure(EntityTypeBuilder<Torrent> builder)
        {
            builder.ToTable(TORRENT_TABLE_NAME);
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Content).IsRequired();
            builder.Property(t => t.ImageUrl);

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