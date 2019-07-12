using System;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Core.Specifications;
using Rutracker.Infrastructure.Data.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.Infrastructure.Repositories
{
    public class TorrentRepositoryTests
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentRepositoryTests()
        {
            var context = MockInitializer.GetTorrentContext();

            _torrentRepository = new TorrentRepository(context);
        }

        [Fact(DisplayName = "GetAsync() should return object with the same id")]
        public async Task GetAsync_5_ReturnsValidTorrent()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrent = await _torrentRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "GetAsync() should return an object based on the specification")]
        public async Task GetAsync_5_ReturnsValidTorrentBySpecification()
        {
            // Arrange
            const long expectedId = 5;
            var specification = new TorrentWithForumAndFilesSpecification(expectedId);

            // Act
            var torrent = await _torrentRepository.GetAsync(specification);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "ListAsync() should return all items")]
        public async Task ListAsync_9_ReturnsValidAllTorrents()
        {
            // Arrange
            const int expectedCount = 9;

            // Act
            var torrents = await _torrentRepository.ListAsync();

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count);
        }

        [Fact(DisplayName = "ListAsync() should return all items based on the specification")]
        public async Task ListAsync_0_5_Null_ReturnsValidTorrentsBySpecification()
        {
            // Arrange
            const int expectedCount = 5;
            var specification = new TorrentsFilterPaginatedSpecification(0, expectedCount,
                null,
                null,
                null,
                null);

            // Act
            var torrents = await _torrentRepository.ListAsync(specification);

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count);
        }

        [Fact(DisplayName = "CountAsync() should return the number of items")]
        public async Task CountAsync_9_ReturnsValidAllItemsCount()
        {
            // Arrange
            const int expectedCount = 9;

            // Act
            var count = await _torrentRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "CountAsync() should return the number of items based on the specification")]
        public async Task CountAsync_0_5_Null_ReturnsValidItemsCountBySpecification()
        {
            // Arrange
            const int expectedCount = 5;
            var specification = new TorrentsFilterPaginatedSpecification(0, expectedCount,
                null,
                null,
                null,
                null);

            // Act
            var count = await _torrentRepository.CountAsync(specification);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetPopularForumsAsync() should return the popular forum title count")]
        public async Task GetPopularForumsAsync_5_ReturnsValidForumTitles()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var forums = await _torrentRepository.GetPopularForumsAsync(expectedCount);

            // Assert
            Assert.NotNull(forums);
            Assert.Equal(expectedCount, forums.Count);
        }

        [Fact(DisplayName = "GetAsync() should return ArgumentNullException")]
        public async Task GetAsync_Null_ReturnsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.GetAsync(null));
        }

        [Fact(DisplayName = "ListAsync() should return ArgumentNullException")]
        public async Task ListAsync_Null_ReturnsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.ListAsync(null));
        }

        [Fact(DisplayName = "CountAsync() should return ArgumentNullException")]
        public async Task CountAsync_Null_ReturnsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.CountAsync(null));
        }
    }
}