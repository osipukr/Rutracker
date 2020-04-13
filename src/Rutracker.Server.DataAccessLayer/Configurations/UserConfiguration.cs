using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.FirstName).HasColumnName("FirstName").HasMaxLength(50);
            builder.Property(user => user.LastName).HasColumnName("LastName").HasMaxLength(50);
            builder.Property(user => user.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(2083);
        }
    }
}