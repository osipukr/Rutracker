using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Infrastructure.Data;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.UnitTests.Setup
{
    public static class MockInitializer
    {
        public static TorrentContext GetTorrentContext()
        {
            var options = new DbContextOptionsBuilder<TorrentContext>().Options;
            var mockContext = new DbContextMock<TorrentContext>(options);

            mockContext.CreateDbSetMock(x => x.Forums, DataInitializer.GetForums());
            mockContext.CreateDbSetMock(x => x.Torrents, DataInitializer.GeTorrents());
            mockContext.CreateDbSetMock(x => x.Files, DataInitializer.GetFiles());

            return mockContext.Object;
        }

        public static ITorrentRepository GetTorrentRepository()
        {
            var mockTorrentRepository = new Mock<ITorrentRepository>();

            var torrents = (IReadOnlyList<Torrent>) DataInitializer.GeTorrents();

            mockTorrentRepository.Setup(x => x.GetAsync(It.IsAny<long>()))
                .Returns<long>(x => Task.FromResult(new Torrent { Id = x }));

            mockTorrentRepository.Setup(x => x.GetAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .Returns<ISpecification<Torrent, long>>(x =>
                    Task.FromResult(torrents.AsQueryable().FirstOrDefault(x.Criteria)));

            mockTorrentRepository.Setup(x => x.ListAsync()).ReturnsAsync(torrents);

            mockTorrentRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .Returns<ISpecification<Torrent, long>>(x =>
                    Task.FromResult((IReadOnlyList<Torrent>)new Torrent[x.Take]));

            mockTorrentRepository.Setup(x => x.CountAsync()).ReturnsAsync(torrents.Count);

            mockTorrentRepository.Setup(x => x.CountAsync(It.IsAny<ISpecification<Torrent, long>>()))
                .Returns<ISpecification<Torrent, long>>(x =>
                    Task.FromResult(torrents.AsQueryable().Where(x.Criteria).Count()));

            mockTorrentRepository.Setup(x => x.GetPopularForumsAsync(It.IsAny<int>()))
                .Returns<int>(x =>
                    Task.FromResult(
                        (IReadOnlyList<(long Id, string Value, int Count)>)new (long, string, int)[x]));

            return mockTorrentRepository.Object;
        }

        public static IMemoryCache GetMemoryCache() => 
            new Mock<MemoryCache>(new Mock<MemoryCacheOptions>().Object).Object;

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