using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.IntegrationTests.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class TorrentsControllerTests : BaseTestController
    {
        private const int TorrentsCount = WebApiContextSeed.TorrentMaxCount;
        private const string SearchPath = "api/torrents/search?page={0}&pageSize={1}";
        private const string PopularPath = "api/torrents/popular?count={0}";
        private const string GetPath = "api/torrents/{0}";

        public TorrentsControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Search_1_5_ReturnsStatus200OK()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = pageSize;
            const int expectedTotalItems = TorrentsCount;

            // Act
            var torrents = await _client.PostJsonAsync<PaginationResult<TorrentViewModel>>(
                string.Format(SearchPath, page, pageSize), new TorrentFilterViewModel());

            // Assert
            Assert.NotNull(torrents);
            Assert.NotNull(torrents.Items);
            Assert.Equal(page, torrents.Page);
            Assert.Equal(pageSize, torrents.PageSize);
            Assert.Equal(expectedTotalItems, torrents.TotalItems);
            Assert.Equal(expectedCount, torrents.Items.Count());
        }

        [Fact]
        public async Task Popular_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = TorrentsCount;

            // Act
            var torrents = await _client.GetJsonAsync<IEnumerable<TorrentViewModel>>(
                string.Format(PopularPath, expectedCount));

            // Assert
            Assert.NotNull(torrents);
            Assert.Equal(expectedCount, torrents.Count());
        }

        [Fact]
        public async Task Get_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = TorrentsCount;

            // Act
            var torrent = await _client.GetJsonAsync<TorrentDetailsViewModel>(string.Format(GetPath, expectedId));

            // Assert
            Assert.NotNull(torrent);
            Assert.NotNull(torrent.Name);
            Assert.NotNull(torrent.Description);
            Assert.Equal(expectedId, torrent.Id);
        }

        [Fact]
        public async Task Search_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<PaginationResult<TorrentViewModel>>(
                    string.Format(SearchPath, -10, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Popular_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<TorrentViewModel>>(string.Format(PopularPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Get_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>(string.Format(GetPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Get_21_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDetailsViewModel>(string.Format(GetPath, TorrentsCount + 1)));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}