using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.UnitTests.Setup
{
    public static class DataInitializer
    {
        public static IEnumerable<Forum> GetTestForums() =>
            new[]
            {
                new Forum { Id = 1, Title = "Forum Title 1" },
                new Forum { Id = 2, Title = "Forum Title 2" },
                new Forum { Id = 3, Title = "Forum Title 3" },
                new Forum { Id = 4, Title = "Forum Title 4" },
                new Forum { Id = 5, Title = "Forum Title 5" }
            };

        public static IEnumerable<Torrent> GeTestTorrents() =>
            new[]
            {
                new Torrent { Id = 1, Title = "Torrent Titles 1", ForumId = 1, Size = 100 },
                new Torrent { Id = 2, Title = "Torrent Titles 12", ForumId = 1, Size = 100 },
                new Torrent { Id = 3, Title = "Torrent Titles 123", ForumId = 1, Size = 1000 },
                new Torrent { Id = 4, Title = "Torrent Titles 1234", ForumId = 2, Size = 1000 },
                new Torrent { Id = 5, Title = "Torrent Titles 12345", ForumId = 2, Size = 10000 },
                new Torrent { Id = 6, Title = "Torrent Titles 123456", ForumId = 3, Size = 10000 },
                new Torrent { Id = 7, Title = "Torrent Titles 1234567", ForumId = 4, Size = 100000 },
                new Torrent { Id = 8, Title = "Torrent Titles 12345678", ForumId = 5, Size = 100000 },
                new Torrent { Id = 9, Title = "Torrent Titles 123456789", ForumId = 5, Size = 100000 },
                new Torrent { Id = 10, Title = "Torrent Titles 123456789", ForumId = 5, Size = 100000 }
            };

        public static IEnumerable<File> GetTestFiles() =>
            new[]
            {
                new File { Id = 1, Size = 100, TorrentId = 1 },
                new File { Id = 2, Size = 100, TorrentId = 1 },
                new File { Id = 3, Size = 100, TorrentId = 1 },
                new File { Id = 4, Size = 100, TorrentId = 1 },
                new File { Id = 5, Size = 100, TorrentId = 2 },
                new File { Id = 6, Size = 100, TorrentId = 2 },
                new File { Id = 7, Size = 100, TorrentId = 3 },
                new File { Id = 8, Size = 100, TorrentId = 4 },
                new File { Id = 9, Size = 100, TorrentId = 4 },
                new File { Id = 10, Size = 100, TorrentId = 4 }
            };
    }
}