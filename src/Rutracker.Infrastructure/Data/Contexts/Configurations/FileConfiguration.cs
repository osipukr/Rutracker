using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Core.Entities;

namespace Rutracker.Infrastructure.Data.Contexts.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();

            builder.HasOne(f => f.Torrent)
                .WithMany(t => t.Files)
                .HasForeignKey(f => f.TorrentId);
        }
    }
}