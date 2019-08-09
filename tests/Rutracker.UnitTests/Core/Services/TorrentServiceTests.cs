using System.Threading.Tasks;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Core.Services;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.Core.Services
{
    public class TorrentServiceTests
    {
        private readonly ITorrentService _torrentService;

        public TorrentServiceTests()
        {
            var repository = MockInitializer.GetTorrentRepository();

            _torrentService = new TorrentService(repository);
        }

        [Fact(DisplayName = "GetTorrentsOnPageAsync() with valid parameters should return the torrents list")]
        public async Task GetTorrentsOnPageAsync_1_10_ReturnsValidTorrents()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var torrents = await _torrentService.GetTorrentsOnPageAsync(1, expectedCount, null, null, null, null);

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count);
        }

        [Fact(DisplayName = "GetTorrentDetailsAsync() with valid parameter should return the torrent with id")]
        public async Task GetTorrentDetailsAsync_5_ReturnsValidTorrent()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrent = await _torrentService.GetTorrentDetailsAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "GetTorrentsCountAsync() with valid parameters should return the number of torrents")]
        public async Task GetTorrentsCountAsync_Null_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = 9;

            // Act
            var count = await _torrentService.GetTorrentsCountAsync(null, null, null, null);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetPopularForumsAsync() with valid parameters should return the torrent forum titles")]
        public async Task GetPopularForumsAsync_10_ReturnsValidForumTitles()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var titles = await _torrentService.GetPopularForumsAsync(expectedCount);

            // Assert
            Assert.NotNull(titles);
            Assert.Equal(expectedCount, titles.Count);
        }

        [Fact(DisplayName = "GetTorrentsOnPageAsync() with an invalid parameters should throw TorrentException")]
        public async Task GetAllTorrentsOnPageAsync_NegativeNumbers_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<TorrentException>(async () =>
                await _torrentService.GetTorrentsOnPageAsync(-10, -10, null, null, null, null));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionType);
        }

        [Fact(DisplayName = "GetTorrentDetailsAsync() with an invalid parameters should throw TorrentException")]
        public async Task GetTorrentDetailsAsync_NegativeNumber_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<TorrentException>(async () =>
                await _torrentService.GetTorrentDetailsAsync(-10));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionType);
        }

        [Fact(DisplayName = "GetTorrentDetailsAsync() with an invalid parameters should throw TorrentException")]
        public async Task GetTorrentDetailsAsync_1000_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<TorrentException>(async () =>
                await _torrentService.GetTorrentDetailsAsync(1000));

            Assert.Equal(ExceptionEventType.NotFound, exception.ExceptionType);
        }

        [Fact(DisplayName = "GetPopularForumsAsync() with an invalid parameters should throw TorrentException")]
        public async Task GetPopularForumsAsync_NegativeNumber_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<TorrentException>(async () =>
                await _torrentService.GetPopularForumsAsync(-10));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionType);
        }
    }
}