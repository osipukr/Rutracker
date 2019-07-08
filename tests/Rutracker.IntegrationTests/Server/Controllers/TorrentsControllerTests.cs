using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;
using Xunit;

namespace Rutracker.IntegrationTests.Server.Controllers
{
    public class TorrentsControllerTests : IClassFixture<ServerFactory>
    {
        private readonly HttpClient _client;

        public TorrentsControllerTests(ServerFactory factory) => _client = factory.CreateClient();

        [Fact(DisplayName = "GetTorrentsIndexAsync() should return torrents index page")]
        public async Task Controller_GetTorrentsIndexAsync_Should_Return_Torrents_Page()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = 5;

            // Act
            var result = await _client.PostJsonAsync<TorrentsIndexViewModel>(
                $"/api/torrents/pagination/?page={page}&pageSize={pageSize}", null);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentItems);
            Assert.NotNull(result.PaginationModel);
            Assert.Equal(page, result.PaginationModel.CurrentPage);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(expectedCount, result.TorrentItems.Length);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync() should return torrent details page")]
        public async Task Controller_GetTorrentIndexAsync_Should_Return_Torrent_Details_Page()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var result = await _client.GetJsonAsync<TorrentIndexViewModel>($"/api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentDetailsItem);
            Assert.Equal(expectedId, result.TorrentDetailsItem.Id);
        }

        [Fact(DisplayName = "GetTitlesAsync() should return forum titles list")]
        public async Task Controller_GetTitlesAsync_Should_Return_Forum_Title_Facets()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var result =
                await _client.GetJsonAsync<FacetViewModel<string>>($"/api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FacetItems);
            Assert.Equal(expectedCount, result.FacetItems.Length);
        }

        [Fact(DisplayName = "GetTorrentsIndexAsync() with negative number should return HttpRequestException")]
        public async Task Controller_GetTorrentsIndexAsync_NegativeNumber_Should_Return_HttpRequestException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.PostJsonAsync<TorrentsIndexViewModel>("/api/torrents/pagination/?page=-10&pageSize=-10",
                    null));
        }

        [Fact(DisplayName = "GetTorrentIndexAsync() with negative number should return HttpRequestException")]
        public async Task Controller_GetTorrentIndexAsync_NegativeNumber_Should_Return_HttpRequestException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentIndexViewModel>("/api/torrents/?id=-10"));
        }

        [Fact(DisplayName = "GetTitlesAsync() with negative number should return HttpRequestException")]
        public async Task Controller_GetTitlesAsync_NegativeNumber_Should_Return_HttpRequestException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FacetViewModel<string>>("/api/torrents/titles/?count=-10"));
        }
    }
}