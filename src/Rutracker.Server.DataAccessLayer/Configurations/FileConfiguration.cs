using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        private const string FILE_TABLE_NAME = "Files";

        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable(FILE_TABLE_NAME);
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Size).IsRequired();
            builder.Property(f => f.Type).IsRequired();
            builder.Property(f => f.Url).IsRequired();

            builder.HasOne(f => f.Torrent)
                .WithMany(t => t.Files)
                .HasForeignKey(f => f.TorrentId);
        }
    }
}