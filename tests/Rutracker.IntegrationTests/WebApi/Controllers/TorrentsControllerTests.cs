using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class TorrentsControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public TorrentsControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact(DisplayName = "Pagination() with valid parameters should return 200OK status")]
        public async Task Pagination_1_5_ReturnsStatus200OK()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = 5;
            var filter = new FilterViewModel();

            // Act
            var result = await _client.PostJsonAsync<PaginationResult<TorrentViewModel>>(
                $"api/torrents/pagination/?page={page}&pageSize={pageSize}", filter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Equal(page, result.Page);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(expectedCount, result.Items.Length);
        }

        [Fact(DisplayName = "Get() with valid parameters should return 200OK status")]
        public async Task Get_5_ReturnsStatus200OK()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var result = await _client.GetJsonAsync<TorrentDetailsViewModel>($"api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact(DisplayName = "Titles() with valid parameters should return 200OK status")]
        public async Task Titles_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var result = await _client.GetJsonAsync<FacetResult<string>>(
                $"api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Equal(expectedCount, result.Items.Length);
        }

        [Fact(DisplayName = "Pagination() with an invalid parameters should return 400BadRequest status")]
        public async Task Pagination_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.PostJsonAsync<PaginationResult<TorrentViewModel>>(
                    "api/torrents/pagination/?page=-10&pageSize=-10", null));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 400BadRequest status")]
        public async Task Get_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>("api/torrents/?id=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 404NotFound status")]
        public async Task Get_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>("api/torrents/?id=1000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Titles() with an invalid parameter should return 400BadRequest status")]
        public async Task Titles_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FacetResult<string>>("api/torrents/titles/?count=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }
    }
}