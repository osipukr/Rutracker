using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");

            builder.HasKey(like => like.Id);

            builder.Property(like => like.Id).ValueGeneratedOnAdd();
            builder.Property(like => like.CommentId);
            builder.Property(like => like.UserId);

            builder.HasOne(like => like.Comment)
                .WithMany(comment => comment.Likes)
                .HasForeignKey(like => like.CommentId);

            builder.HasOne(like => like.User)
                .WithMany(user => user.Likes)
                .HasForeignKey(like => like.UserId);
        }
    }
}