using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");

            builder.HasKey(file => file.Id);

            builder.HasIndex(file => file.BlobName).IsUnique();

            builder.Property(file => file.Id).ValueGeneratedOnAdd();
            builder.Property(file => file.Name).IsRequired();
            builder.Property(file => file.BlobName).IsRequired();
            builder.Property(file => file.Size).IsRequired();
            builder.Property(file => file.Type).IsRequired();
            builder.Property(file => file.Url);
            builder.Property(file => file.TorrentId);

            builder.HasOne(file => file.Torrent)
                .WithMany(torrent => torrent.Files)
                .HasForeignKey(file => file.TorrentId);
        }
    }
}