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
            var context = MockInitializer.GetRutrackerContext();

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
            const int expectedId = 5;

            // Act
            var torrentId = (await _torrentRepository.GetAsync(expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, torrentId);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid torrent.")]
        public async Task GetAsync_5_ReturnsValidTorrentByExpression()
        {
            // Arrange
            const int expectedId = 5;

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

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _torrentRepository.ExistAsync(10);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _torrentRepository.ExistAsync(x => x.Id == 10);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "Search() with valid parameter should return the number of items.")]
        public void Search_4_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = 4;

            // Act
            var count = _torrentRepository.Search("1234567", null, null).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "PopularTorrents() with valid parameter should return the popular torrent count.")]
        public void PopularTorrents_5_ReturnsValidPopularTorrents()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var count = _torrentRepository.PopularTorrents(expectedCount).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}