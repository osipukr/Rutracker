using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.IntegrationTests.WebApi
{
    public class WebApiContextSeed
    {
        private static readonly string UserId = Guid.NewGuid().ToString();

        private readonly RutrackerContext _context;

        public WebApiContextSeed(RutrackerContext context) => _context = context;

        public const int CategoryMaxCount = 10;
        public const int SubcategoryMaxCount = 30;
        public const int TorrentMaxCount = 100;
        public const int FileMaxCount = 200;
        public const int CommentMaxCount = 100;

        public async Task SeedAsync()
        {
            await _context.Categories.AddRangeAsync(GetPreconfiguredCategories());
            await _context.Subcategories.AddRangeAsync(GetPreconfiguredSubcategories());
            await _context.Torrents.AddRangeAsync(GetPreconfiguredTorrents());
            await _context.Files.AddRangeAsync(GetPreconfiguredFiles());
            await _context.Comments.AddRangeAsync(GetPreconfiguredComments());

            await _context.SaveChangesAsync();
        }

        private static IEnumerable<Category> GetPreconfiguredCategories() =>
            Enumerable.Range(1, CategoryMaxCount).Select(id => new Category
            {
                Name = Guid.NewGuid().ToString()
            });

        private static IEnumerable<Subcategory> GetPreconfiguredSubcategories() =>
            Enumerable.Range(1, SubcategoryMaxCount).Select(id => new Subcategory
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = 1
            });

        private static IEnumerable<Torrent> GetPreconfiguredTorrents() =>
            Enumerable.Range(1, TorrentMaxCount).Select(id => new Torrent
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                SubcategoryId = 1,
                UserId = UserId
            });

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            Enumerable.Range(1, FileMaxCount).Select(id => new File
            {
                Name = Guid.NewGuid().ToString(),
                Type = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                TorrentId = 1
            });

        private static IEnumerable<Comment> GetPreconfiguredComments() =>
            Enumerable.Range(1, CommentMaxCount).Select(id => new Comment
            {
                Text = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                TorrentId = 1,
                UserId = UserId
            });
    }
}