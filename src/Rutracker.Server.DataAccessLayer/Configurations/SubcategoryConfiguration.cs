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

            builder.HasIndex(subcategory => subcategory.Name).IsUnique();

            builder.Property(subcategory => subcategory.Id).ValueGeneratedOnAdd();
            builder.Property(subcategory => subcategory.Name).IsRequired();
            builder.Property(subcategory => subcategory.Description);
            builder.Property(subcategory => subcategory.AddedDate).IsRequired();
            builder.Property(subcategory => subcategory.ModifiedDate);
            builder.Property(subcategory => subcategory.CategoryId);

            builder.HasOne(subcategory => subcategory.Category)
                .WithMany(category => category.Subcategories)
                .HasForeignKey(subcategory => subcategory.CategoryId);

            builder.HasMany(subcategory => subcategory.Torrents)
                .WithOne(torrent => torrent.Subcategory)
                .HasForeignKey(torrent => torrent.SubcategoryId);
        }
    }
}