using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class ForumConfiguration : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedNever().IsRequired();

            builder.HasMany(f => f.Torrents)
                .WithOne(t => t.Forum)
                .HasForeignKey(t => t.ForumId);
        }
    }
}