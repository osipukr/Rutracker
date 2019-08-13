using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class TorrentContextSeed
    {
        public static async Task SeedAsync(TorrentContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await context.Forums.AnyAsync())
                {
                    await context.Forums.AddRangeAsync(GetPreconfiguredForums());
                    await context.SaveChangesAsync();
                }

                if (!await context.Torrents.AnyAsync())
                {
                    await context.Torrents.AddRangeAsync(GetPreconfiguredTorrents());
                    await context.SaveChangesAsync();
                }

                if (!await context.Files.AnyAsync())
                {
                    await context.Files.AddRangeAsync(GetPreconfiguredFiles());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<TorrentContextSeed>().LogError(ex.Message);
            }
        }

        #region Generate preconfigured items

        private static readonly Random Random = new Random();
        private const int ForumMaxCount = 50;
        private const int TorrentMaxCount = 500;
        private const int FileMaxCount = 1000;

        private static IEnumerable<Forum> GetPreconfiguredForums() =>
            Enumerable.Range(1, ForumMaxCount).Select(id => new Forum
            {
                Id = id,
                Title = Guid.NewGuid().ToString()
            });

        private static IEnumerable<Torrent> GetPreconfiguredTorrents() =>
            Enumerable.Range(1, TorrentMaxCount).Select(id => new Torrent
            {
                Id = id,
                Date = DateTime.Now,
                Size = Random.Next(1, int.MaxValue),
                Title = Guid.NewGuid().ToString(),
                Hash = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                TrackerId = Random.Next(1, int.MaxValue),
                ForumId = Random.Next(1, ForumMaxCount)
            });

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            Enumerable.Range(1, FileMaxCount).Select(id => new File
            {
                Name = Guid.NewGuid().ToString(),
                Size = Random.Next(1, int.MaxValue),
                TorrentId = Random.Next(1, TorrentMaxCount)
            });

        #endregion
    }
}