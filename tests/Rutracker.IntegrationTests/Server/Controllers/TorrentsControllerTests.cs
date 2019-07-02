using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rutracker.IntegrationTests.Response;
using Rutracker.Server;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;
using Xunit;

namespace Rutracker.IntegrationTests.Server.Controllers
{
    public class TorrentsControllerTests : IClassFixture<ServerFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TorrentsControllerTests(ServerFactory<Startup> factory) => _client = factory.CreateClient();

        [Fact(DisplayName = "GetTorrentsIndexAsync(page,pageSize,filter) should return torrents index page")]
        public async Task Controller_GetTorrentsIndexAsync_Should_Return_Torrents_Page()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = 5;

            // Act
            var response = await _client.PostJsonAsync<OkResponse<TorrentsIndexViewModel>>(
                $"/api/torrents/pagination/?page={page}&pageSize={pageSize}", null);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Value);
            Assert.NotNull(response.Value.TorrentItems);
            Assert.NotNull(response.Value.PaginationModel);
            Assert.Equal(page, response.Value.PaginationModel.CurrentPage);
            Assert.Equal(pageSize, response.Value.PaginationModel.PageSize);
            Assert.Equal(expectedCount, response.Value.TorrentItems.Length);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync(id) should return torrent details page")]
        public async Task Controller_GetTorrentIndexAsync_Should_Return_Torrent_Details_Page()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var response =
                await _client.GetJsonAsync<OkResponse<TorrentIndexViewModel>>($"/api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Value.TorrentDetailsItem);
            Assert.Equal(expectedId, response.Value.TorrentDetailsItem.Id);
        }

        [Fact(DisplayName = "GetTitlesAsync(count) should return forum titles list")]
        public async Task Controller_GetTitlesAsync_Should_Return_Forum_Title_Facets()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var response =
                await _client.GetJsonAsync<OkResponse<FacetViewModel>>($"/api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Value.FacetItems);
            Assert.Equal(expectedCount, response.Value.FacetItems.Length);
        }

        [Fact(DisplayName = "GetTorrentsIndexAsync() with negative number should return BadRequestResponse")]
        public async Task Controller_GetTorrentsIndexAsync_NegativeNumber_Should_Return_BadRequestResponse()
        {
            // Arrange
            const int page = -10;
            const int pageSize = 10;

            // Act
            var response = await _client.PostJsonAsync<BadRequestResponse>(
                $"/api/torrents/pagination/?page={page}&pageSize={pageSize}", null);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync() with negative number should return BadRequestResponse")]
        public async Task Controller_GetTorrentIndexAsync_NegativeNumber_Should_Return_BadRequestResponse()
        {
            // Arrange
            const long id = -10;

            // Act
            var response = await _client.GetJsonAsync<BadRequestResponse>($"/api/torrents/?id={id}");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Fact(DisplayName = "GetTitlesAsync() with negative number should return BadRequestResponse")]
        public async Task Controller_GetTitlesAsync_NegativeNumber_Should_Return_BadRequestResponse()
        {
            // Arrange
            const int count = -10;

            // Act
            var response = await _client.GetJsonAsync<BadRequestResponse>($"/api/torrents/titles/?count={count}");

            // Assert
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Message);
        }
    }
}