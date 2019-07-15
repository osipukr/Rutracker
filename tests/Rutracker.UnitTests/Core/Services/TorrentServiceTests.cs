using System.Threading.Tasks;
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

        [Fact(DisplayName = "GetAllTorrentsOnPageAsync() with valid parameters should return the torrents list")]
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
        public async Task GetTorrentsCountAsync_Nulls_ReturnsValidCount()
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
    }
}