using Microsoft.EntityFrameworkCore;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.UnitTests.TestData
{
    public static class TestDatabase
    {
        public static TorrentContext GetContext(string name)
        {
            var options = new DbContextOptionsBuilder<TorrentContext>()
                .UseInMemoryDatabase(name)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var context = new TorrentContext(options);

            context.SeedAsync().Wait();

            return context;
        }
    }
}