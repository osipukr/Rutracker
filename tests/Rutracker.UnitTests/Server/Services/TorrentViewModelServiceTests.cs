using System.Threading.Tasks;
using Rutracker.Server.Interfaces.Services;
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

        [Fact(DisplayName = "GetTorrentsIndexAsync() should return the torrents page")]
        public async Task GetTorrentsIndexAsync_1_10_Null_ReturnsValidTorrentsIndexPage()
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

        [Fact(DisplayName = "GetTorrentIndexAsync() should return a torrent page for a specific id")]
        public async Task GetTorrentIndexAsync_5_ReturnsValidTorrentIndexPage()
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

        [Fact(DisplayName = "GetTitleFacetAsync() should return a count of forum titles")]
        public async Task GetTitleFacetAsync_5_ReturnsValidTitleFacet()
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
    }
}