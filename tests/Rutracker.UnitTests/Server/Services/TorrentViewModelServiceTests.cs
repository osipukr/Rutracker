using System;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.Server.Services
{
    public class TorrentViewModelServiceTests
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentViewModelServiceTests()
        {
            var service = MockInitializer.GeTorrentService();
            var cache = MockInitializer.GetMemoryCache();
            var mapper = MockInitializer.GeMapper();
            var options = MockInitializer.GetCacheOptions();

            _torrentViewModelService = new TorrentViewModelService(service, mapper, cache, options);
        }

        [Fact(DisplayName = "GetTorrentsIndexAsync(page,size,filter) should return the torrents page")]
        public async Task ViewService_GetTorrentsIndexAsync_Should_Return_Torrents_Page()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;

            // Act
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, null);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentItems);
            Assert.Equal(pageSize, result.TorrentItems.Length);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync(id) should return a torrent page for a specific id")]
        public async Task ViewService_GetTorrentIndexAsync_Should_Return_Torrent_Page()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var result = await _torrentViewModelService.GetTorrentIndexAsync(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentDetailsItem);
            Assert.Equal(expectedId, result.TorrentDetailsItem.Id);
        }

        [Fact(DisplayName = "GetTitlesAsync(count) should return a count of forum titles")]
        public async Task ViewService_GetTitlesAsync_Should_Return_ForumTitle_Facet()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var result = await _torrentViewModelService.GetTitleFacetAsync(expectedCount);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FacetItems);
            Assert.Equal(expectedCount, result.FacetItems.Length);
        }

        [Fact(DisplayName =
            "GetTorrentsIndexAsync(page,size,filter) with negative page number should return ArgumentOutOfRangeException")]
        public async Task ViewService_GetTorrentsIndexAsync_NegativePageNumber_Should_Return_ArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _torrentViewModelService.GetTorrentsIndexAsync(-10, 10, null));
        }

        [Fact(DisplayName =
            "GetTorrentsIndexAsync(page,size,filter) with negative pageSize number should return ArgumentOutOfRangeException")]
        public async Task ViewService_GetTorrentsIndexAsync_NegativePageSizeNumber_Should_Return_ArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _torrentViewModelService.GetTorrentsIndexAsync(10, -10, null));
        }

        [Fact(DisplayName =
            "GetTorrentIndexAsync(id) with negative id number should return ArgumentOutOfRangeException")]
        public async Task ViewService_GetTorrentIndexAsync_NegativePageNumber_Should_Return_ArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _torrentViewModelService.GetTorrentIndexAsync(-10));
        }

        [Fact(DisplayName =
            "GetTitlesAsync(count) with negative count number should return ArgumentOutOfRangeException")]
        public async Task ViewService_GetTitlesAsync_NegativePageNumber_Should_Return_ArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _torrentViewModelService.GetTitleFacetAsync(-10));
        }
    }
}