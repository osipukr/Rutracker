using System.Collections.Generic;
using Rutracker.Core.Entities;

namespace Rutracker.UnitTests
{
    public static class DataSeed
    {
        public static IEnumerable<Torrent> GetTestTorrentItems() =>
            new[]
            {
                new Torrent {ForumId = 10, Title = "0", Size = 1000},
                new Torrent {ForumId = 10, Title = "01", Size = 75},
                new Torrent {ForumId = 20, Title = "012", Size = 10},
                new Torrent {ForumId = 30, Title = "0123", Size = 100},
                new Torrent {ForumId = 40, Title = "01234", Size = 250},
                new Torrent {ForumId = 60, Title = "012345", Size = 130},
                new Torrent {ForumId = 70, Title = "0123456", Size = 750},
                new Torrent {ForumId = 70, Title = "01234567", Size = 2000},
                new Torrent {ForumId = 70, Title = "012345678", Size = 750},
                new Torrent {ForumId = 70, Title = "0123456789", Size = 5060},
                new Torrent {ForumId = 70, Title = "a", Size = 10000},
                new Torrent {ForumId = 70, Title = "ab", Size = 10000},
                new Torrent {ForumId = 100, Title = "abc", Size = 750},
                new Torrent {ForumId = 110, Title = "abcd", Size = 1000},
                new Torrent {ForumId = 110, Title = "abcde", Size = 75},
                new Torrent {ForumId = 110, Title = "abcdef", Size = 130},
                new Torrent {ForumId = 220, Title = "abcdefg", Size = 10},
                new Torrent {ForumId = 330, Title = "abcdefgh", Size = 100},
                new Torrent {ForumId = 450, Title = "abcdefghi", Size = 250},
                new Torrent {ForumId = 900, Title = "abcdefghij", Size = 750}
            };
    }
}