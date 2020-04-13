using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable("Subcategories");

            builder.HasKey(subcategory => subcategory.Id);

            builder.HasIndex(subcategory => subcategory.Name).HasName("Name");

            builder.Property(subcategory => subcategory.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(subcategory => subcategory.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(subcategory => subcategory.Description).HasColumnName("Description").HasMaxLength(200);
            builder.Property(subcategory => subcategory.AddedDate).HasColumnName("AddedDate").HasColumnType("datetime");
            builder.Property(subcategory => subcategory.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime");
            builder.Property(subcategory => subcategory.CategoryId).HasColumnName("CategoryId");

            builder.HasOne(subcategory => subcategory.Category)
                .WithMany(category => category.Subcategories)
                .HasForeignKey(subcategory => subcategory.CategoryId);
        }
    }
}