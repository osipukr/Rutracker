using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
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

        [Fact(DisplayName = "Pagination() with valid parameters should return 200OK status")]
        public async Task Pagination_1_5_ReturnsStatus200OK()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = 5;
            var filter = new FiltrationViewModel();

            // Act
            var result = await _client.PostJsonAsync<TorrentsIndexViewModel>(
                $"api/torrents/pagination/?page={page}&pageSize={pageSize}", filter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentItems);
            Assert.NotNull(result.PaginationModel);
            Assert.Equal(page, result.PaginationModel.CurrentPage);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(expectedCount, result.TorrentItems.Length);
        }

        [Fact(DisplayName = "Get() with valid parameters should return 200OK status")]
        public async Task Get_5_ReturnsStatus200OK()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var result = await _client.GetJsonAsync<TorrentIndexViewModel>($"api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentDetailsItem);
            Assert.Equal(expectedId, result.TorrentDetailsItem.Id);
        }

        [Fact(DisplayName = "Titles() with valid parameters should return 200OK status")]
        public async Task Titles_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var result = await _client.GetJsonAsync<FacetViewModel<string>>(
                $"api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FacetItems);
            Assert.Equal(expectedCount, result.FacetItems.Length);
        }

        [Fact(DisplayName = "Pagination() with an invalid parameters should return 400BadRequest status")]
        public async Task Pagination_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.PostJsonAsync<TorrentsIndexViewModel>(
                    "api/torrents/pagination/?page=-10&pageSize=-10", null));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 400BadRequest status")]
        public async Task Get_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentIndexViewModel>("api/torrents/?id=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 404NotFound status")]
        public async Task Get_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentIndexViewModel>("api/torrents/?id=1000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Titles() with an invalid parameter should return 400BadRequest status")]
        public async Task Titles_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FacetViewModel<string>>("api/torrents/titles/?count=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }
    }
}