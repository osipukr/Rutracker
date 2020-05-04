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

            builder.HasIndex(category => category.Name).IsUnique();

            builder.Property(category => category.Id).ValueGeneratedOnAdd();
            builder.Property(category => category.Name).HasMaxLength(100).IsRequired();
            builder.Property(category => category.Description).HasMaxLength(200);
            builder.Property(category => category.AddedDate).IsRequired();
            builder.Property(category => category.ModifiedDate);

            builder.HasMany(category => category.Subcategories)
                .WithOne(subcategory => subcategory.Category)
                .HasForeignKey(subcategory => subcategory.CategoryId);
        }
    }
}