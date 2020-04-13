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

            builder.Property(comment => comment.Id).HasColumnName("CommentID").ValueGeneratedOnAdd();
            builder.Property(comment => comment.Text).HasColumnName("Text");
            builder.Property(comment => comment.AddedDate).HasColumnName("AddedDate").HasColumnType("datetime");
            builder.Property(comment => comment.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime");
            builder.Property(comment => comment.TorrentId).HasColumnName("TorrentID");
            builder.Property(comment => comment.UserId).HasColumnName("UserID");

            builder.HasOne(comment => comment.Torrent)
                .WithMany(torrent => torrent.Comments)
                .HasForeignKey(comment => comment.TorrentId);

            builder.HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId);
        }
    }
}