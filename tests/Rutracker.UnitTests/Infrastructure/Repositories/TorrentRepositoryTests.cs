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

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid torrent")]
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

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid torrent")]
        public async Task GetAsync_5BySpecification_ReturnsValidTorrent()
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

        [Fact(DisplayName = "ListAsync() should return all items based on the specification")]
        public async Task ListAsync_0_5_NullsBySpecification_ReturnsValidTorrentsBySpecification()
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

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items")]
        public async Task CountAsync_0_5_Nulls_ReturnsValidCountBySpecification()
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

        [Fact(DisplayName = "GetPopularForumsAsync() with valid parameter should return the popular forum title count")]
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

        [Fact(DisplayName = "GetAsync() with an invalid parameter should throw ArgumentNullException")]
        public async Task GetAsync_Null_ThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.GetAsync(null));
        }

        [Fact(DisplayName = "ListAsync() with an invalid parameter should throw ArgumentNullException")]
        public async Task ListAsync_Null_ThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.ListAsync(null));
        }

        [Fact(DisplayName = "CountAsync() with an invalid parameter should throw ArgumentNullException")]
        public async Task CountAsync_Null_ThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.CountAsync(null));
        }
    }
}