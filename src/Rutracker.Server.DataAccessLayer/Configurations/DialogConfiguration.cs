using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class DialogConfiguration : IEntityTypeConfiguration<Dialog>
    {
        public void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder.Property(d => d.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(d => d.Title).IsRequired();

            builder.HasOne(d => d.User)
                .WithMany(u => u.Dialogs)
                .HasForeignKey(d => d.UserId);

            builder.HasMany(d => d.Messages)
                .WithOne(m => m.Dialog)
                .HasForeignKey(m => m.DialogId);
        }
    }
}