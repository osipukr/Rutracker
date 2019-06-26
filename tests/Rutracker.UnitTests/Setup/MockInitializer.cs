using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;

namespace Rutracker.UnitTests.Setup
{
    public static class MockInitializer
    {
        public static TorrentContext GetTorrentContext()
        {
            //var mockContext = new Mock<TorrentContext>();

            //mockContext.Setup(x => x.Forums)
            //    .Returns(GetQueryableMockDbSet(TorrentContextExtensions.GetPreconfiguredForums()));

            //mockContext.Setup(x => x.Torrents)
            //    .Returns(GetQueryableMockDbSet(TorrentContextExtensions.GetPreconfiguredTorrents()));

            //mockContext.Setup(x => x.Files)
            //    .Returns(GetQueryableMockDbSet(TorrentContextExtensions.GetPreconfiguredFiles()));

            //return mockContext.Object;

            var options = new DbContextOptionsBuilder<TorrentContext>().Options;
            var mockContext = new DbContextMock<TorrentContext>(options);

            mockContext.CreateDbSetMock(x => x.Forums, TorrentContextExtensions.GetPreconfiguredForums());
            mockContext.CreateDbSetMock(x => x.Torrents, TorrentContextExtensions.GetPreconfiguredTorrents());
            mockContext.CreateDbSetMock(x => x.Files, TorrentContextExtensions.GetPreconfiguredFiles());

            return mockContext.Object;
        }

        public static ITorrentRepository GetTorrentRepository()
        {
            var mockTorrentRepository = new Mock<ITorrentRepository>();

            // setup logic

            return mockTorrentRepository.Object;
        }

        //private static DbSet<T> GetQueryableMockDbSet<T>(IEnumerable<T> source) 
        //    where T : class
        //{
        //    var dbSet = new Mock<DbSet<T>>();
        //    var queryable = source.AsQueryable();

        //    dbSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryable.Provider);
        //    dbSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryable.Expression);
        //    dbSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
        //    dbSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator);

        //    return dbSet.Object;
        //}
    }
}