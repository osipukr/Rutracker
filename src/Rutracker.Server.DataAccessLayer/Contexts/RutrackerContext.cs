using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class RutrackerContext : IdentityDbContext<User, Role, string>
    {
        public RutrackerContext()
        {
        }

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
            builder.ApplyConfigurationsFromAssembly(typeof(RutrackerContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}