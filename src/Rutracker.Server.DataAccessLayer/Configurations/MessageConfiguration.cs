using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(m => m.Text).IsRequired();
            builder.Property(m => m.SentAt).IsRequired();

            builder.HasOne(m => m.Dialog)
                .WithMany(d => d.Messages)
                .HasForeignKey(m => m.DialogId);

            builder.HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);
        }
    }
}