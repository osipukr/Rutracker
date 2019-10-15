using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class UserDialogConfiguration : IEntityTypeConfiguration<UserDialog>
    {
        public void Configure(EntityTypeBuilder<UserDialog> builder)
        {
            builder.HasKey(ud => new { ud.UserId, ud.DialogId });

            builder.HasOne(ud => ud.User)
                .WithMany(u => u.UserDialogs)
                .HasForeignKey(ud => ud.UserId);

            builder.HasOne(ud => ud.Dialog)
                .WithMany(d => d.UserDialogs)
                .HasForeignKey(ud => ud.DialogId);
        }
    }
}