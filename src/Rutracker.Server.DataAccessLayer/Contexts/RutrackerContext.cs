using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Configurations;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class RutrackerContext : IdentityDbContext<User, Role, string>
    {
        public RutrackerContext(DbContextOptions<RutrackerContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<Torrent> Torrents { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration())
                   .ApplyConfiguration(new CategoryConfiguration())
                   .ApplyConfiguration(new SubcategoryConfiguration())
                   .ApplyConfiguration(new TorrentConfiguration())
                   .ApplyConfiguration(new FileConfiguration())
                   .ApplyConfiguration(new CommentConfiguration())
                   .ApplyConfiguration(new LikeConfiguration());
        }
    }
}