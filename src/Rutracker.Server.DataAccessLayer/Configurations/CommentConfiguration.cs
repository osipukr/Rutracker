using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(comment => comment.Id);

            builder.Property(comment => comment.Id).ValueGeneratedOnAdd();
            builder.Property(comment => comment.Text).IsRequired();
            builder.Property(comment => comment.AddedDate).IsRequired();
            builder.Property(comment => comment.ModifiedDate);
            builder.Property(comment => comment.TorrentId);
            builder.Property(comment => comment.UserId);

            builder.HasOne(comment => comment.Torrent)
                .WithMany(torrent => torrent.Comments)
                .HasForeignKey(comment => comment.TorrentId);

            builder.HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId);

            builder.HasMany(comment => comment.Likes)
                .WithOne(like => like.Comment)
                .HasForeignKey(like => like.CommentId);
        }
    }
}