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

            builder.Property(file => file.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(file => file.Name).HasColumnName("Name").HasMaxLength(150).IsRequired();
            builder.Property(file => file.Size).HasColumnName("Size").IsRequired();
            builder.Property(file => file.TorrentId).HasColumnName("TorrentId");

            builder.HasOne(file => file.Torrent)
                .WithMany(torrent => torrent.Files)
                .HasForeignKey(file => file.TorrentId);
        }
    }
}