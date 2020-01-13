using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        private const string ROLE_TABLE_NAME = "Roles";

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(ROLE_TABLE_NAME);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
        }
    }
}