using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;

namespace Rutracker.Infrastructure.Data.Extensions
{
    public static class TorrentContextExtensions
    {
        public static async Task SeedAsync(this TorrentContext context)
        {
            try
            {
                if (!await context.Forums.AnyAsync())
                {
                    var files = GetPreconfiguredForums();

                    await context.Forums.AddRangeAsync(files);
                    await context.SaveChangesAsync();
                }

                if (!await context.Torrents.AnyAsync())
                {
                    var torrents = GetPreconfiguredTorrents();

                    await context.Torrents.AddRangeAsync(torrents);
                    await context.SaveChangesAsync();
                }

                if (!await context.Files.AnyAsync())
                {
                    var files = GetPreconfiguredFiles();

                    await context.Files.AddRangeAsync(files);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // logger
                Console.WriteLine(ex.Message);
            }
        }

        #region GetPreconfiguredItems

        private static IEnumerable<Forum> GetPreconfiguredForums() =>
            new[]
            {
                new Forum {Id = 1, Title = "Forum Title 1"},
                new Forum {Id = 2, Title = "Forum Title 2"},
                new Forum {Id = 3, Title = "Forum Title 3"},
                new Forum {Id = 4, Title = "Forum Title 4"},
                new Forum {Id = 5, Title = "Forum Title 5"},
                new Forum {Id = 6, Title = "Forum Title 6"},
                new Forum {Id = 7, Title = "Forum Title 7"},
                new Forum {Id = 8, Title = "Forum Title 8"},
                new Forum {Id = 9, Title = "Forum Title 9"},
                new Forum {Id = 10, Title = "Forum Title 10"}
            };

        private static IEnumerable<Torrent> GetPreconfiguredTorrents() =>
            new[]
            {
                new Torrent
                {
                    Id = 1,
                    Date = new DateTime(2001, 1, 1),
                    Size = 100000,
                    Title = "Torrent Title 1",
                    Hash = "aasdsa23dsad1sadsa231asf",
                    Content = "Torrent Content 1",
                    TrackerId = 1,
                    ForumId = 1,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 2,
                    Date = new DateTime(2002, 2, 2),
                    Size = 1000002,
                    Title = "Torrent Title 2",
                    Hash = "aasdsa23dsad1sadsa231asf",
                    Content = "Torrent Content 2",
                    TrackerId = 2,
                    ForumId = 2,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 3,
                    Date = new DateTime(2003, 3, 3),
                    Size = 1000003,
                    Title = "Torrent Title 3",
                    Hash = "aasdsa23dsad1sadsa231asf",
                    Content = "Torrent Content 3",
                    TrackerId = 3,
                    ForumId = 1,
                    IsDeleted = true
                },
                new Torrent
                {
                    Id = 4,
                    Date = new DateTime(2004, 4, 4),
                    Size = 100040,
                    Title = "Torrent Title ",
                    Hash = "aasdsa23dsasadad1sadsa231asf",
                    Content = "Torrent Content 4",
                    TrackerId = 4,
                    ForumId = 4,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 5,
                    Date = new DateTime(2005, 5, 5),
                    Size = 1000005,
                    Title = "Torrent Title 5",
                    Hash = "aasdsasa23dsad1sadsa231asf",
                    Content = "Torrent Content 5",
                    TrackerId = 5,
                    ForumId = 5,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 6,
                    Date = new DateTime(2006, 6, 6),
                    Size = 1000006,
                    Title = "Torrent Title 6",
                    Hash = "aasdsa23dsad1sadsa231asf",
                    Content = "Torrent Content 6",
                    TrackerId = 1,
                    ForumId = 6,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 7,
                    Date = new DateTime(2007, 7, 10),
                    Size = 100000,
                    Title = "Torrent Title 7",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 70as",
                    TrackerId = 10,
                    ForumId = 10,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 8,
                    Date = new DateTime(2007, 7, 10),
                    Size = 1000020,
                    Title = "Torrent Title 8",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 8",
                    TrackerId = 10,
                    ForumId = 8,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 9,
                    Date = new DateTime(2017, 7, 10),
                    Size = 100000,
                    Title = "Torrent Title 8",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 1",
                    TrackerId = 10,
                    ForumId = 9,
                    IsDeleted = true
                },
                new Torrent
                {
                    Id = 10,
                    Date = new DateTime(2009, 7, 10),
                    Size = 101204,
                    Title = "Torrent Title 8",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 9",
                    TrackerId = 5,
                    ForumId = 5,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 11,
                    Date = new DateTime(2010, 7, 10),
                    Size = 100000,
                    Title = "Torrent Title 8",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 1",
                    TrackerId = 8,
                    ForumId = 10,
                    IsDeleted = false
                },
                new Torrent
                {
                    Id = 12,
                    Date = new DateTime(2019, 7, 10),
                    Size = 102500,
                    Title = "Torrent Title 8",
                    Hash = "aaa2sd2sa3sa23d1sad1sad1AsTa231asf",
                    Content = "Torrent Content 10",
                    TrackerId = 120,
                    ForumId = 5,
                    IsDeleted = false
                }
            };

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            new[]
            {
                new File {Id = 1, Name = "File Name 1", Size = 100, TorrentId = 1},
                new File {Id = 2, Name = "File Name 2", Size = 100, TorrentId = 1},
                new File {Id = 3, Name = "File Name 3", Size = 100, TorrentId = 1},
                new File {Id = 4, Name = "File Name 4", Size = 100, TorrentId = 1},
                new File {Id = 5, Name = "File Name 5", Size = 100, TorrentId = 2},
                new File {Id = 6, Name = "File Name 6", Size = 100, TorrentId = 2},
                new File {Id = 7, Name = "File Name 7", Size = 100, TorrentId = 3},
                new File {Id = 8, Name = "File Name 8", Size = 100, TorrentId = 4},
                new File {Id = 9, Name = "File Name 9", Size = 100, TorrentId = 4},
                new File {Id = 10, Name = "File Name 10", Size = 100, TorrentId = 5},
                new File {Id = 11, Name = "File Name 11", Size = 100, TorrentId = 6},
                new File {Id = 12, Name = "File Name 12", Size = 100, TorrentId = 6},
                new File {Id = 13, Name = "File Name 13", Size = 100, TorrentId = 6},
                new File {Id = 14, Name = "File Name 14", Size = 100, TorrentId = 7},
                new File {Id = 15, Name = "File Name 15", Size = 100, TorrentId = 8},
                new File {Id = 16, Name = "File Name 16", Size = 100, TorrentId = 8},
                new File {Id = 17, Name = "File Name 17", Size = 100, TorrentId = 8},
                new File {Id = 18, Name = "File Name 18", Size = 100, TorrentId = 9},
                new File {Id = 19, Name = "File Name 19", Size = 100, TorrentId = 9},
                new File {Id = 20, Name = "File Name 20", Size = 100, TorrentId = 9},
                new File {Id = 21, Name = "File Name 21", Size = 100, TorrentId = 9},
                new File {Id = 22, Name = "File Name 22", Size = 100, TorrentId = 10},
                new File {Id = 23, Name = "File Name 23", Size = 100, TorrentId = 10},
                new File {Id = 24, Name = "File Name 24", Size = 100, TorrentId = 10},
                new File {Id = 25, Name = "File Name 25", Size = 100, TorrentId = 11},
                new File {Id = 26, Name = "File Name 26", Size = 100, TorrentId = 11},
                new File {Id = 27, Name = "File Name 27", Size = 100, TorrentId = 11}
            };

        #endregion
    }
}