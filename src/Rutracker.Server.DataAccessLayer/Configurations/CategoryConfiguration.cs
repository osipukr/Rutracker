using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private const string CATEGORY_TABLE_NAME = "Categories";

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(CATEGORY_TABLE_NAME);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.Name).IsRequired();

            builder.HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}