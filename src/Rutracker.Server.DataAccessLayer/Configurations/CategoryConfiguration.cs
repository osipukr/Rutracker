using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(category => category.Id);

            builder.HasIndex(category => category.Name).HasName("Name").IsUnique();

            builder.Property(category => category.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(category => category.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(category => category.Description).HasColumnName("Description").HasMaxLength(200);
            builder.Property(category => category.AddedDate).HasColumnName("AddedDate").HasColumnType("datetime");
            builder.Property(category => category.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime");
        }
    }
}