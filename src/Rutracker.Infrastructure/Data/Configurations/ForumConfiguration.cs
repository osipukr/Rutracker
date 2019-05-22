using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Core.Entities;

namespace Rutracker.Infrastructure.Data.Configurations
{
    public class ForumConfiguration : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.ToTable("Forums");
            builder.Property(t => t.Id).ValueGeneratedNever().IsRequired();

            builder.HasMany(f => f.Torrents)
                .WithOne(t => t.Forum)
                .HasForeignKey(t => t.ForumId);
        }
    }
}