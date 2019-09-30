using System;
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
        private const int TorrentsCount = DataInitializer.TorrentsCount;

        public TorrentRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _torrentRepository = new TorrentRepository(context);
        }

        [Fact]
        public void GetAll_10_ReturnsValidTorrents()
        {
            // Arrange
            const int expectedCount = TorrentsCount;

            // Act
            var torrents = _torrentRepository.GetAll();

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count());
        }

        [Fact]
        public void GetAll_10_ReturnsValidTorrentsByExpression()
        {
            // Arrange
            const int expectedId = 1;

            // Act
            var torrents = _torrentRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(torrents);
            Assert.Single(torrents);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidTorrent()
        {
            // Arrange
            const int expectedId = TorrentsCount;

            // Act
            var torrent = await _torrentRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.NotNull(torrent.Name);
            Assert.NotNull(torrent.Description);
            Assert.NotNull(torrent.UserId);
            Assert.InRange(torrent.SubcategoryId, 1, int.MaxValue);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidTorrentByExpression()
        {
            // Arrange
            const int expectedId = TorrentsCount;

            // Act
            var torrent = await _torrentRepository.GetAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(torrent);
            Assert.NotNull(torrent.Name);
            Assert.NotNull(torrent.Description);
            Assert.NotNull(torrent.UserId);
            Assert.InRange(torrent.SubcategoryId, 1, int.MaxValue);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = TorrentsCount;

            // Act
            var count = await _torrentRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = TorrentsCount;
            const int expectedCount = 1;

            // Act
            var count = await _torrentRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = TorrentsCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _torrentRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = TorrentsCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _torrentRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}