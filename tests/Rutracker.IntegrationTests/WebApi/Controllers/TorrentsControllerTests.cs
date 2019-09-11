using System.Collections.Generic;
using System.Linq;
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

        [Fact(DisplayName = "Pagination() with valid parameters should return 200OK status.")]
        public async Task Pagination_1_5_ReturnsStatus200OK()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = 5;

            // Act
            var torrents = await _client.PostJsonAsync<PaginationResult<TorrentViewModel>>(
                $"api/torrents/pagination/?page={page}&pageSize={pageSize}", new TorrentFilterViewModel());

            // Assert
            Assert.NotNull(torrents);
            Assert.NotNull(torrents.Items);
            Assert.Equal(page, torrents.Page);
            Assert.Equal(pageSize, torrents.PageSize);
            Assert.Equal(expectedCount, torrents.Items.Length);
        }

        [Fact(DisplayName = "Popular() with valid parameters should return 200OK status.")]
        public async Task Popular_10_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var torrents = await _client.GetJsonAsync<IEnumerable<TorrentShortViewModel>>(
                $"api/torrents/popular/?count={expectedCount}");

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count());
        }

        [Fact(DisplayName = "Get() with valid parameters should return 200OK status.")]
        public async Task Get_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var torrent = await _client.GetJsonAsync<TorrentDetailsViewModel>($"api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(torrent);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact(DisplayName = "Pagination() with an invalid parameters should return 400BadRequest status.")]
        public async Task Pagination_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<PaginationResult<TorrentViewModel>>("api/torrents/popular/?count=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Popular() with an invalid parameters should return 400BadRequest status.")]
        public async Task Popular_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<TorrentShortViewModel>>("api/torrents/popular/?count=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 400BadRequest status.")]
        public async Task Get_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>("api/torrents/?id=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Get() with an invalid parameter should return 404NotFound status.")]
        public async Task Get_10000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>("api/torrents/?id=10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}