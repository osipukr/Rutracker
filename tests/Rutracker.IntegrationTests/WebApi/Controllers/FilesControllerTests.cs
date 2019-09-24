using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models.ViewModels.File;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class FilesControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public FilesControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact]
        public async Task Search_1_ReturnsStatus200OK()
        {
            const int torrentId = 1;

            // Act
            var files = await _client.GetJsonAsync<IEnumerable<FileViewModel>>($"api/files/search?torrentId={torrentId}");

            // Assert
            Assert.NotNull(files);
        }

        [Fact]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var file = await _client.GetJsonAsync<FileViewModel>($"api/subcategories/{expectedId}");

            // Assert
            Assert.NotNull(file);
            Assert.Equal(expectedId, file.Id);
        }

        [Fact]
        public async Task Search_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<FileViewModel>>("api/files/search?torrentId=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FileViewModel>("api/files/-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<FileViewModel>("api/files/10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}