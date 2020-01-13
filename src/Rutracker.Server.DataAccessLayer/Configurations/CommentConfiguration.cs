using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        private const string COMMENT_TABLE_NAME = "Comments";

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(COMMENT_TABLE_NAME);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.Text).IsRequired();

            builder.HasOne(c => c.Torrent)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TorrentId);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.Likes)
                .WithOne(l => l.Comment)
                .HasForeignKey(l => l.CommentId);
        }
    }
}