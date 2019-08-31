using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.UnitTests.Setup
{
    public static class DataInitializer
    {
        public static IEnumerable<Category> GetTestCategories() =>
            new[]
            {
                new Category { Id = 1 },
                new Category { Id = 2 },
                new Category { Id = 3 },
                new Category { Id = 4 },
                new Category { Id = 5 },
                new Category { Id = 6 },
                new Category { Id = 7 },
                new Category { Id = 8 },
                new Category { Id = 9 },
                new Category { Id = 10 }
            };

        public static IEnumerable<Subcategory> GetTestSubcategories() =>
            new[]
            {
                new Subcategory { Id = 1, CategoryId = 1 },
                new Subcategory { Id = 2, CategoryId = 2 },
                new Subcategory { Id = 3, CategoryId = 2 },
                new Subcategory { Id = 4, CategoryId = 4 },
                new Subcategory { Id = 5, CategoryId = 5 },
                new Subcategory { Id = 6, CategoryId = 5 },
                new Subcategory { Id = 7, CategoryId = 5 },
                new Subcategory { Id = 8, CategoryId = 8 },
                new Subcategory { Id = 9, CategoryId = 9 },
                new Subcategory { Id = 10, CategoryId = 10  }
            };

        public static IEnumerable<Torrent> GeTestTorrents() =>
            new[]
            {
                new Torrent { Id = 1, Name = "1", UserId = "1", SubcategoryId = 5 },
                new Torrent { Id = 2, Name = "12", UserId = "2", SubcategoryId = 5 },
                new Torrent { Id = 3, Name = "123", UserId = "3", SubcategoryId = 5 },
                new Torrent { Id = 4, Name = "1234", UserId = "4", SubcategoryId = 5 },
                new Torrent { Id = 5, Name = "12345", UserId = "5", SubcategoryId = 5 },
                new Torrent { Id = 6, Name = "123456", UserId = "6", SubcategoryId = 2 },
                new Torrent { Id = 7, Name = "1234567", UserId = "7", SubcategoryId = 2 },
                new Torrent { Id = 8, Name = "12345678", UserId = "8", SubcategoryId = 2 },
                new Torrent { Id = 9, Name = "123456789", UserId = "9", SubcategoryId = 2 },
                new Torrent { Id = 10, Name = "123456789", UserId = "10", SubcategoryId = 1 }
            };

        public static IEnumerable<File> GetTestFiles() =>
            new[]
            {
                new File { Id = 1, TorrentId = 1 },
                new File { Id = 2, TorrentId = 1 },
                new File { Id = 3, TorrentId = 1 },
                new File { Id = 4, TorrentId = 1 },
                new File { Id = 5, TorrentId = 2 },
                new File { Id = 6, TorrentId = 2 },
                new File { Id = 7, TorrentId = 3 },
                new File { Id = 8, TorrentId = 4 },
                new File { Id = 9, TorrentId = 4 },
                new File { Id = 10, TorrentId = 4 }
            };

        public static IEnumerable<Comment> GetTestComments() =>
            new[]
            {
                new Comment { Id = 1, TorrentId = 1, UserId = "1", Likes = new List<Like>() },
                new Comment { Id = 2, TorrentId = 1, UserId = "1", Likes = new List<Like>() },
                new Comment { Id = 3, TorrentId = 1, UserId = "1", Likes = new List<Like>() },
                new Comment { Id = 4, TorrentId = 2, UserId = "4", Likes = new List<Like>() },
                new Comment { Id = 5, TorrentId = 2, UserId = "5", Likes = new List<Like>() },
                new Comment { Id = 6, TorrentId = 3, UserId = "6", Likes = new List<Like>() },
                new Comment { Id = 7, TorrentId = 4, UserId = "7", Likes = new List<Like>() },
                new Comment { Id = 8, TorrentId = 6, UserId = "8", Likes = new List<Like>() },
                new Comment { Id = 9, TorrentId = 8, UserId = "9", Likes = new List<Like>() },
                new Comment { Id = 10, TorrentId = 10, UserId = "10", Likes = new List<Like>() }
            };

        public static IEnumerable<Like> GetTestLikes() =>
            new[]
            {
                new Like { Id = 1, CommentId = 1, UserId = "1" },
                new Like { Id = 2, CommentId = 1, UserId = "2" },
                new Like { Id = 3, CommentId = 1, UserId = "3" },
                new Like { Id = 4, CommentId = 1, UserId = "4" },
                new Like { Id = 5, CommentId = 1, UserId = "5" }
            };
    }
}