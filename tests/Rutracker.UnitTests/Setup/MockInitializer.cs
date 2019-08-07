using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Rutracker.Core.Entities.Torrents;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Core.Interfaces.Specifications;
using Rutracker.Infrastructure.Data.Contexts;
using Rutracker.Server.Settings;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;
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
            var torrents = DataInitializer.GeTestTorrents().AsQueryable();

            mockTorrentRepository.Setup(x => x.GetAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .ReturnsAsync((ISpecification<Torrent, long> x) => torrents.FirstOrDefault(x.Criteria));

            mockTorrentRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .ReturnsAsync((ISpecification<Torrent, long> x) => new Torrent[x.Take]);

            mockTorrentRepository.Setup(x => x.CountAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .ReturnsAsync((ISpecification<Torrent, long> x) => torrents.Where(x.Criteria).Count());

            mockTorrentRepository.Setup(x => x.GetPopularForumsAsync(It.IsAny<int>()))
                .ReturnsAsync((int x) => new (long, string, int)[x]);

            return mockTorrentRepository.Object;
        }

        public static ITorrentService GeTorrentService()
        {
            var mockTorrentService = new Mock<ITorrentService>();

            mockTorrentService.Setup(x =>
                    x.GetTorrentsOnPageAsync(It.IsInRange(0, int.MaxValue, Range.Exclusive),
                        It.IsInRange(0, int.MaxValue, Range.Exclusive), null, null, null, null))
                .ReturnsAsync((int page, int pageSize, string search, IEnumerable<string> titles,
                    long? sizeFrom, long? sizeTo) => new Torrent[pageSize]);

            mockTorrentService.Setup(x => x.GetTorrentDetailsAsync(It.IsInRange(0, long.MaxValue, Range.Exclusive)))
                .ReturnsAsync((long x) => new Torrent { Id = x });

            mockTorrentService.Setup(x => x.GetTorrentsCountAsync(null, null, null, null))
                .ReturnsAsync(DataInitializer.GeTestTorrents().Count());

            mockTorrentService.Setup(x => x.GetPopularForumsAsync(It.IsInRange(0, int.MaxValue, Range.Exclusive)))
                .ReturnsAsync((int x) => new (long, string, int)[x]);

            mockTorrentService.Setup(x =>
                    x.GetTorrentsOnPageAsync(It.IsInRange(int.MinValue, 0, Range.Inclusive),
                        It.IsInRange(int.MinValue, 0, Range.Inclusive), null, null, null, null))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            mockTorrentService.Setup(x => x.GetTorrentDetailsAsync(It.IsInRange(long.MinValue, 0, Range.Inclusive)))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            mockTorrentService.Setup(x => x.GetPopularForumsAsync(It.IsInRange(int.MinValue, 0, Range.Inclusive)))
                .ThrowsAsync(new TorrentException(string.Empty, ExceptionEventType.NotValidParameters));

            return mockTorrentService.Object;
        }

        public static IMemoryCache GetMemoryCache() =>
            new Mock<MemoryCache>(new Mock<MemoryCacheOptions>().Object).Object;

        public static IOptions<CacheSettings> GetCacheOptions() =>
            Mock.Of<IOptions<CacheSettings>>(x => x.Value == new CacheSettings
            {
                CacheDuration = TimeSpan.MaxValue
            });

        public static IMapper GeMapper()
        {
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<TorrentItemViewModel[]>(It.IsAny<Torrent[]>()))
                .Returns<Torrent[]>(x => new TorrentItemViewModel[x.Length]);

            mockMapper.Setup(x => x.Map<TorrentDetailsItemViewModel>(It.IsAny<Torrent>()))
                .Returns<Torrent>(x => new TorrentDetailsItemViewModel { Id = x.Id });

            return mockMapper.Object;
        }
    }
}