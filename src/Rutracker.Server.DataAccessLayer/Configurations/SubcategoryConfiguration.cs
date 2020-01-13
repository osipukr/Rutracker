using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        private const string SUBCATEGORY_TABLE_NAME = "Subcategories";

        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable(SUBCATEGORY_TABLE_NAME);
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(s => s.Name).IsRequired();

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(s => s.CategoryId);

            builder.HasMany(s => s.Torrents)
                .WithOne(t => t.Subcategory)
                .HasForeignKey(t => t.SubcategoryId);
        }
    }
}