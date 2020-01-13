using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        private const string LIKE_TABLE_NAME = "Likes";

        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable(LIKE_TABLE_NAME);
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd().IsRequired();

            builder.HasOne(l => l.Comment)
                .WithMany(c => c.Likes)
                .HasForeignKey(l => l.CommentId);

            builder.HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);
        }
    }
}