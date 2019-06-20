using Microsoft.EntityFrameworkCore;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.IntegrationTests
{
    public class TestDatabase
    {
        public static TorrentContext GetDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<TorrentContext>()
                .UseInMemoryDatabase(name)
                .Options;

            var context = new TorrentContext(options);

            context.SeedAsync().Wait();

            return context;
        }
    }
}