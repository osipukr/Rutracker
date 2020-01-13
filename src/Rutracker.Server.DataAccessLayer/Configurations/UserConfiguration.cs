using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private const string USER_TABLE_NAME = "Users";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(USER_TABLE_NAME);
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.ImageUrl);
            builder.Property(u => u.IsRegistrationFinished);

            builder.HasMany(u => u.Torrents)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);
        }
    }
}