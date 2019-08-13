using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class TorrentRepositoryTests
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentRepositoryTests()
        {
            var context = MockInitializer.GetTorrentContext();

            _torrentRepository = new TorrentRepository(context);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid torrents.")]
        public void GetAll_10_ReturnsValidTorrents()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var count = _torrentRepository.GetAll().Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid torrents.")]
        public void GetAll_10_ReturnsValidTorrentsByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = _torrentRepository.GetAll(x => x.Id == 10).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid torrent.")]
        public async Task GetAsync_5_ReturnsValidTorrent()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrentId = (await _torrentRepository.GetAsync(expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, torrentId);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid torrent.")]
        public async Task GetAsync_5_ReturnsValidTorrentByExpression()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrentId = (await _torrentRepository.GetAsync(x => x.Id == expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, torrentId);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var count = await _torrentRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = await _torrentRepository.CountAsync(x => x.Id == 10);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "Search() with valid parameter should return the number of items.")]
        public void Search_5_ReturnsValidCount()
        {
            // Arrange
            var search = "Torrent";
            var forumIds = new[] { 5L };
            const int expectedCount = 3;

            // Act
            var count = _torrentRepository.Search(search, forumIds, null, null).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetForums() with valid parameter should return the popular forum title count.")]
        public void GetForums_5_ReturnsValidForumTitles()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var count = _torrentRepository.GetForums(expectedCount).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}