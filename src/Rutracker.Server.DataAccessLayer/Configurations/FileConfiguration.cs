using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Url).IsRequired();

            builder.HasOne(f => f.Torrent)
                .WithMany(t => t.Files)
                .HasForeignKey(f => f.TorrentId);
        }
    }
}