using System;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Infrastructure.Data;
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

        [Fact(DisplayName = "GetAsync(id) should return object with the same id")]
        public async void Repository_GetAsync_Should_Find_Item_By_Id()
        {
            // Arrange
            const long expectedId = 10;

            // Act
            var torrent = await _torrentRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "GetAsync(spec) should return an object based on the specification")]
        public async void Repository_GetAsync_Should_Find_Item_By_Specification()
        {
            // Arrange
            const long expectedId = 10;
            var specification = new TorrentWithForumAndFilesSpecification(expectedId);

            // Act
            var torrent = await _torrentRepository.GetAsync(specification);

            // Assert
            Assert.NotNull(torrent);
            Assert.NotNull(torrent.Forum);
            Assert.NotNull(torrent.Files);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "ListAsync() should return all items")]
        public async void Repository_ListAsync_Should_Return_All_Items()
        {
            // Arrange
            const int expectedCount = 12;

            // Act
            var torrents = await _torrentRepository.ListAsync();

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count);
        }

        [Fact(DisplayName = "ListAsync(spec) should return all items based on the specification")]
        public async void Repository_ListAsync_Should_Return_All_Items_By_Specification()
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
        public async void Repository_CountAsync_Should_Return_Number_Of_Items()
        {
            // Arrange
            const int expectedCount = 12;

            // Act
            var count = await _torrentRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "CountAsync() should return the number of items based on the specification")]
        public async void Repository_CountAsync_Should_Return_Number_Of_Items_By_Specification()
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

        [Fact(DisplayName = "GetPopularForumsAsync(count) should return the popular forum title count")]
        public async void Repository_GetPopularForumsAsync_Should_Return_N_Popular_Forum_Title()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var forums = await _torrentRepository.GetPopularForumsAsync(expectedCount);

            // Assert
            Assert.NotNull(forums);
            Assert.Equal(expectedCount, forums.Count);
        }

        [Fact(DisplayName = "GetAsync(null) should return ArgumentNullException")]
        public async void Repository_GetAsync_Null_Should_Return_ArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.GetAsync(null));
        }

        [Fact(DisplayName = "ListAsync(null) should return ArgumentNullException")]
        public async void Repository_ListAsync_Null_Should_Return_ArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.ListAsync(null));
        }

        [Fact(DisplayName = "CountAsync(null) should return ArgumentNullException")]
        public async void Repository_CountAsync_Null_Should_Return_ArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _torrentRepository.CountAsync(null));
        }
    }
}