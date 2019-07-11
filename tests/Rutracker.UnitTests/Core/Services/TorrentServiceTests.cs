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

        [Fact(DisplayName = "GetAllTorrentsOnPageAsync() should return the torrents list on page")]
        public async Task Service_GetTorrentsOnPageAsync_Should_Return_Torrents_List()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var torrents = await _torrentService.GetTorrentsOnPageAsync(1, expectedCount, null, null, null, null);

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count);
        }

        [Fact(DisplayName = "GetTorrentDetailsAsync(id) should return the torrent with id")]
        public async Task Service_GetTorrentDetailsAsync_Should_Return_Torrent_Details()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrent = await _torrentService.GetTorrentDetailsAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "GetTorrentsCountAsync() should return the number of torrents")]
        public async Task Service_GetTorrentsCountAsync_Should_Return_Torrents_Count()
        {
            // Arrange
            const int expectedCount = 9;

            // Act
            var count = await _torrentService.GetTorrentsCountAsync(null, null, null, null);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetPopularForumsAsync(count) should return the torrent forum titles")]
        public async Task Service_GetPopularForumsAsync_Should_Return_TorrentForum_Titles()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var titles = await _torrentService.GetPopularForumsAsync(expectedCount);

            // Assert
            Assert.NotNull(titles);
            Assert.Equal(expectedCount, titles.Count);
        }

        [Fact(DisplayName = "GetTorrentsOnPageAsync() with negative number should return GenericException")]
        public async Task Service_GetTorrentsOnPageAsync_NegativeNumber_Should_Return_GenericException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<TorrentException>(async () =>
                await _torrentService.GetTorrentsOnPageAsync(-10, -10, null, null, null, null));
        }

        [Fact(DisplayName = "GetTorrentDetailsAsync() with negative id number should return GenericException")]
        public async Task Service_GetTorrentDetailsAsync_NegativePageNumber_Should_Return_GenericException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<TorrentException>(async () => await _torrentService.GetTorrentDetailsAsync(-10));
        }

        [Fact(DisplayName = "GetPopularForumsAsync() with negative count number should return GenericException")]
        public async Task Service_GetPopularForumsAsync_NegativeNumber_Should_Return_GenericException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<TorrentException>(async () => await _torrentService.GetPopularForumsAsync(-10));
        }
    }
}