using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.IntegrationTests.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.File;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class FilesControllerTests : BaseTestController
    {
        private const int TorrentsCount = WebApiContextSeed.TorrentMaxCount;
        private const int FilesCount = WebApiContextSeed.FileMaxCount;
        private const string SearchPath = "api/files/search?torrentId={0}";
        private const string FindPath = "api/files/{0}";

        public FilesControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Search_20_ReturnsStatus200OK()
        {
            // Arrange
            const int torrentId = TorrentsCount;
            const int expectedCount = FilesCount;

            // Act
            var files = await _client.GetJsonAsync<IEnumerable<FileViewModel>>(string.Format(SearchPath, torrentId));

            // Assert
            Assert.NotNull(files);
            Assert.Equal(expectedCount, files.Count());
        }

        [Fact]
        public async Task Find_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = FilesCount;

            // Act
            var file = await _client.GetJsonAsync<FileViewModel>(string.Format(FindPath, expectedId));

            // Assert
            Assert.NotNull(file);
            Assert.NotNull(file.Name);
            Assert.NotNull(file.Type);
            Assert.Equal(expectedId, file.Id);
        }

        [Fact]
        public async Task Search_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<FileViewModel>>(string.Format(SearchPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FileViewModel>(string.Format(FindPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_21_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FileViewModel>(string.Format(FindPath, FilesCount + 1)));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}