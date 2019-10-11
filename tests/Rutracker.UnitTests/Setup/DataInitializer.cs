using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.UnitTests.Setup
{
    public static class DataInitializer
    {
        public const int CategoriesCount = 10;
        public const int SubcategoriesCount = 10;
        public const int TorrentsCount = 10;
        public const int FilesCount = 10;
        public const int CommentsCount = 10;
        public const int LikesCount = 5;

        public static IEnumerable<Category> GetCategories() =>
            new[]
            {
                new Category {Id = 1, Name = ""},
                new Category {Id = 2, Name = ""},
                new Category {Id = 3, Name = ""},
                new Category {Id = 4, Name = ""},
                new Category {Id = 5, Name = ""},
                new Category {Id = 6, Name = ""},
                new Category {Id = 7, Name = ""},
                new Category {Id = 8, Name = ""},
                new Category {Id = 9, Name = ""},
                new Category {Id = 10, Name = ""}
            };

        public static IEnumerable<Subcategory> GetSubcategories() =>
            new[]
            {
                new Subcategory {Id = 1, CategoryId = 1, Name = ""},
                new Subcategory {Id = 2, CategoryId = 2, Name = ""},
                new Subcategory {Id = 3, CategoryId = 2, Name = ""},
                new Subcategory {Id = 4, CategoryId = 4, Name = ""},
                new Subcategory {Id = 5, CategoryId = 5, Name = ""},
                new Subcategory {Id = 6, CategoryId = 5, Name = ""},
                new Subcategory {Id = 7, CategoryId = 5, Name = ""},
                new Subcategory {Id = 8, CategoryId = 8, Name = ""},
                new Subcategory {Id = 9, CategoryId = 9, Name = ""},
                new Subcategory {Id = 10, CategoryId = 10, Name = ""}
            };

        public static IEnumerable<Torrent> GeTorrents() =>
            new[]
            {
                new Torrent {Id = 1, Name = "", Description = "", UserId = "1", SubcategoryId = 5},
                new Torrent {Id = 2, Name = "", Description = "", UserId = "2", SubcategoryId = 5},
                new Torrent {Id = 3, Name = "", Description = "", UserId = "3", SubcategoryId = 5},
                new Torrent {Id = 4, Name = "", Description = "", UserId = "4", SubcategoryId = 5},
                new Torrent {Id = 5, Name = "", Description = "", UserId = "5", SubcategoryId = 5},
                new Torrent {Id = 6, Name = "", Description = "", UserId = "6", SubcategoryId = 2},
                new Torrent {Id = 7, Name = "", Description = "", UserId = "7", SubcategoryId = 2},
                new Torrent {Id = 8, Name = "", Description = "", UserId = "8", SubcategoryId = 2},
                new Torrent {Id = 9, Name = "", Description = "", UserId = "9", SubcategoryId = 2},
                new Torrent {Id = 10, Name = "", Description = "", UserId = "10", SubcategoryId = 1}
            };

        public static IEnumerable<File> GetFiles() =>
            new[]
            {
                new File {Id = 1, TorrentId = 1, Name = "", Type = "", Url = ""},
                new File {Id = 2, TorrentId = 1, Name = "", Type = "", Url = ""},
                new File {Id = 3, TorrentId = 1, Name = "", Type = "", Url = ""},
                new File {Id = 4, TorrentId = 1, Name = "", Type = "", Url = ""},
                new File {Id = 5, TorrentId = 2, Name = "", Type = "", Url = ""},
                new File {Id = 6, TorrentId = 2, Name = "", Type = "", Url = ""},
                new File {Id = 7, TorrentId = 3, Name = "", Type = "", Url = ""},
                new File {Id = 8, TorrentId = 4, Name = "", Type = "", Url = ""},
                new File {Id = 9, TorrentId = 4, Name = "", Type = "", Url = ""},
                new File {Id = 10, TorrentId = 4, Name = "", Type = "", Url = ""}
            };

        public static IEnumerable<Comment> GetComments() =>
            new[]
            {
                new Comment {Id = 1, TorrentId = 1, UserId = "1", Text = ""},
                new Comment {Id = 2, TorrentId = 1, UserId = "1", Text = ""},
                new Comment {Id = 3, TorrentId = 1, UserId = "1", Text = ""},
                new Comment {Id = 4, TorrentId = 2, UserId = "4", Text = ""},
                new Comment {Id = 5, TorrentId = 2, UserId = "5", Text = ""},
                new Comment {Id = 6, TorrentId = 3, UserId = "6", Text = ""},
                new Comment {Id = 7, TorrentId = 4, UserId = "7", Text = ""},
                new Comment {Id = 8, TorrentId = 6, UserId = "8", Text = ""},
                new Comment {Id = 9, TorrentId = 8, UserId = "9", Text = ""},
                new Comment {Id = 10, TorrentId = 10, UserId = "10", Text = ""}
            };

        public static IEnumerable<Like> GetLikes() =>
            new[]
            {
                new Like {Id = 1, CommentId = 1, UserId = "1"},
                new Like {Id = 2, CommentId = 1, UserId = "2"},
                new Like {Id = 3, CommentId = 1, UserId = "3"},
                new Like {Id = 4, CommentId = 1, UserId = "4"},
                new Like {Id = 5, CommentId = 1, UserId = "5"}
            };
    }
}