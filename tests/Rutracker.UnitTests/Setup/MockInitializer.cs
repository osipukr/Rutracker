using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.WebApi.Settings;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Range = Moq.Range;

namespace Rutracker.UnitTests.Setup
{
    public static class MockInitializer
    {
        public static TorrentContext GetTorrentContext()
        {
            var options = new DbContextOptionsBuilder<TorrentContext>().Options;
            var mockContext = new DbContextMock<TorrentContext>(options);

            mockContext.CreateDbSetMock(x => x.Forums, DataInitializer.GetTestForums());
            mockContext.CreateDbSetMock(x => x.Torrents, DataInitializer.GeTestTorrents());
            mockContext.CreateDbSetMock(x => x.Files, DataInitializer.GetTestFiles());

            return mockContext.Object;
        }

        public static ITorrentRepository GetTorrentRepository()
        {
            var mockTorrentRepository = new Mock<ITorrentRepository>();
            var torrents = new DbQueryMock<Torrent>(DataInitializer.GeTestTorrents()).Object;

            mockTorrentRepository.Setup(x => x.GetAll()).Returns(torrents);

            mockTorrentRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .Returns<Expression<Func<Torrent, bool>>>(torrents.Where);

            mockTorrentRepository.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync((long id) => torrents.FirstOrDefault(x => x.Id == id));

            mockTorrentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .ReturnsAsync(torrents.FirstOrDefault);

            mockTorrentRepository.Setup(x => x.CountAsync()).ReturnsAsync(torrents.Count);

            mockTorrentRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Torrent, bool>>>()))
                .ReturnsAsync(torrents.Count);

            mockTorrentRepository.Setup(x => x.GetForums(It.IsAny<int>()))
                .Returns<int>(x => new DbQueryMock<Tuple<long, string, int>>(new Tuple<long, string, int>[x]).Object);

            mockTorrentRepository.Setup(x => x.Search(null, null, null, null))
                .Returns<string, long[], long?, long?>((search, forumIds, sizeFrom, sizeTo) => torrents);

            return mockTorrentRepository.Object;
        }

        public static ITorrentService GeTorrentService()
        {
            var mockTorrentService = new Mock<ITorrentService>();

            mockTorrentService.Setup(x =>
                    x.ListAsync(It.IsInRange(0, int.MaxValue, Range.Exclusive),
                        It.IsInRange(0, int.MaxValue, Range.Exclusive), null, null, null, null))
                .ReturnsAsync((int page, int pageSize, string search, IEnumerable<string> titles,
                    long? sizeFrom, long? sizeTo) => new Torrent[pageSize]);

            mockTorrentService.Setup(x => x.FindAsync(It.IsInRange(0, long.MaxValue, Range.Exclusive)))
                .ReturnsAsync((long x) => new Torrent { Id = x });

            mockTorrentService.Setup(x => x.CountAsync(null, null, null, null))
                .ReturnsAsync(DataInitializer.GeTestTorrents().Count());

            mockTorrentService.Setup(x => x.ForumsAsync(It.IsInRange(0, int.MaxValue, Range.Exclusive)))
                .ReturnsAsync((int x) => Enumerable.Range(0, x)
                    .Select(t => new Tuple<long, string, int>(long.MinValue, string.Empty, int.MinValue)));

            mockTorrentService.Setup(x =>
                    x.ListAsync(It.IsInRange(int.MinValue, 0, Range.Inclusive),
                        It.IsInRange(int.MinValue, 0, Range.Inclusive), null, null, null, null))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            mockTorrentService.Setup(x => x.FindAsync(It.IsInRange(long.MinValue, 0, Range.Inclusive)))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            mockTorrentService.Setup(x => x.ForumsAsync(It.IsInRange(int.MinValue, 0, Range.Inclusive)))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            return mockTorrentService.Object;
        }

        public static IMemoryCache GetMemoryCache() =>
            new Mock<MemoryCache>(new Mock<MemoryCacheOptions>().Object).Object;

        public static IOptions<CacheSettings> GetCacheOptions()
        {
            var mockCacheSettings = new Mock<IOptions<CacheSettings>>();

            mockCacheSettings.Setup(x => x.Value).Returns(new CacheSettings
            {
                CacheDuration = TimeSpan.MaxValue
            });

            return mockCacheSettings.Object;
        }

        public static IMapper GeMapper()
        {
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<TorrentViewModel[]>(It.IsAny<Torrent[]>()))
                .Returns<Torrent[]>(x => new TorrentViewModel[x.Length]);

            mockMapper.Setup(x => x.Map<TorrentDetailsViewModel>(It.IsAny<Torrent>()))
                .Returns<Torrent>(x => new TorrentDetailsViewModel { Id = x.Id });

            return mockMapper.Object;
        }
    }
}