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

        [Fact(DisplayName = "Pagination() should return torrents index page")]
        public async Task Pagination_ValidParameters_ReturnsStatus200OK()
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

        [Fact(DisplayName = "Get() should return torrent details page")]
        public async Task Get_ValidParameters_ReturnsStatus200OK()
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

        [Fact(DisplayName = "Titles() should return forum titles list")]
        public async Task Titles_ValidParameters_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var result =
                await _client.GetJsonAsync<FacetViewModel<string>>($"api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FacetItems);

            Assert.Equal(expectedCount, result.FacetItems.Length);
        }

        [Fact(DisplayName = "Pagination() with negative number should return HttpRequestException")]
        public async Task Pagination_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.PostJsonAsync<TorrentsIndexViewModel>("api/torrents/pagination/?page=-10&pageSize=-10",
                    null));
        }

        [Fact(DisplayName = "Get() with negative number should return HttpRequestException")]
        public async Task Get_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentIndexViewModel>("api/torrents/?id=-10"));
        }

        [Fact(DisplayName = "Titles() with negative number should return HttpRequestException")]
        public async Task Titles_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FacetViewModel<string>>("api/torrents/titles/?count=-10"));
        }
    }
}