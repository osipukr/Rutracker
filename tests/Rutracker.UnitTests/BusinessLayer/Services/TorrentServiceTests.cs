using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class TorrentServiceTests
    {
        private readonly ITorrentService _torrentService;
        private const int TorrentsCount = DataInitializer.TorrentsCount;

        public TorrentServiceTests()
        {
            var torrentRepository = MockInitializer.GetTorrentRepository();
            var subcategoryRepository = MockInitializer.GetSubcategoryRepository();
            var fileStorageService = MockInitializer.GetFileStorageService();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _torrentService =
                new TorrentService(torrentRepository, subcategoryRepository, fileStorageService, unitOfWork);
        }

        [Fact]
        public async Task ListAsync_1_9_ReturnsValidTorrents()
        {
            // Arrange
            const int expectedCount = TorrentsCount - 1;
            const int expectedTotalCount = TorrentsCount;

            // Act
            var torrents = await _torrentService.ListAsync(1, expectedCount, null, null, null);

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Item1.Count());
            Assert.Equal(expectedTotalCount, torrents.Item2);
        }

        [Fact]
        public async Task PopularAsync_10_ReturnsValidTorrents()
        {
            // Arrange
            const int expectedCount = TorrentsCount;

            // Act
            var torrents = await _torrentService.PopularAsync(expectedCount);

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count());
        }

        [Fact]
        public async Task FindAsync_10_ReturnsValidTorrent()
        {
            // Arrange
            const int expectedId = TorrentsCount;

            // Act
            var torrent = await _torrentService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact]
        public async Task ListAsync_NegativeNumbers_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.ListAsync(-10, -10, null, null, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task PopularAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.PopularAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.FindAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_11_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.FindAsync(TorrentsCount + 1));

            Assert.Equal(ExceptionEventTypes.NotFound, exception.ExceptionEventType);
        }

        [Fact]
        public async Task AddAsync_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.AddAsync(null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task UpdateAsync_NegativeNumber_Nulls_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.UpdateAsync(-10, null, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task DeleteAsync_NegativeNumber_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _torrentService.DeleteAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}