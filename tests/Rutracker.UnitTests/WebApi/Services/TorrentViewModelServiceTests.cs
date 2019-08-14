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

        [Fact(DisplayName = "TorrentsAsync() with valid parameters should return torrents.")]
        public async Task TorrentsAsync_1_10_ReturnsValidTorrentsIndexViewModel()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;
            const int expectedCount = 10;
            var filter = new FilterViewModel();

            // Act
            var torrents = await _torrentViewModelService.TorrentsAsync(page, pageSize, filter);

            // Assert
            Assert.NotNull(torrents);
            Assert.NotNull(torrents.Items);
            Assert.Equal(page, torrents.Page);
            Assert.Equal(pageSize, torrents.PageSize);
            Assert.Equal(expectedCount, torrents.Items.Length);
        }

        [Fact(DisplayName = "TorrentAsync() with valid parameter should return torrent details.")]
        public async Task TorrentAsync_5_ReturnsValidTorrentIndexViewModel()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var torrent = await _torrentViewModelService.TorrentAsync(expectedId);

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "ForumFacetAsync() with valid parameter should return forum facets.")]
        public async Task ForumFacetAsync_5_ReturnsValidFacetViewModel()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var forumFacets = await _torrentViewModelService.ForumFacetAsync(expectedCount);

            // Assert
            Assert.NotNull(forumFacets);
            Assert.NotNull(forumFacets.Items);
            Assert.Equal(expectedCount, forumFacets.Items.Length);
        }
    }
}