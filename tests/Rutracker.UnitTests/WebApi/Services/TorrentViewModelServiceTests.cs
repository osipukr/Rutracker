using System.Threading.Tasks;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Services;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.WebApi.Services
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
        public async Task GetTorrentsIndexAsync_1_10_ReturnsValidTorrentsIndexViewModel()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;
            const int expectedCount = 10;
            var filter = new FilterViewModel();

            // Act
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Equal(page, result.Page);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(expectedCount, result.Items.Length);
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
            Assert.Equal(expectedId, result.Id);
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
            Assert.NotNull(result.Items);
            Assert.Equal(expectedCount, result.Items.Length);
        }
    }
}