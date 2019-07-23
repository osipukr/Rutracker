using System.Threading.Tasks;
using Rutracker.Server.Services;
using Rutracker.Shared.Interfaces;
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

        [Fact(DisplayName = "GetTorrentsIndexAsync() with valid parameters should return TorrentsIndexViewMode")]
        public async Task GetTorrentsIndexAsync_1_10_Null_ReturnsValidTorrentsIndexViewModel()
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

        [Fact(DisplayName = "GetTorrentIndexAsync() with valid parameter should return TorrentIndexViewModel")]
        public async Task GetTorrentIndexAsync_5_ReturnsValidTorrentIndexViewModel()
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

        [Fact(DisplayName = "GetTitleFacetAsync() with valid parameter should return FacetViewModel")]
        public async Task GetTitleFacetAsync_5_ReturnsValidFacetViewModel()
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