using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class CategoriesControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact(DisplayName = "List() with valid parameters should return 200OK status.")]
        public async Task List_10_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var categories = await _client.GetJsonAsync<IEnumerable<CategoryViewModel>>("api/categories");

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact(DisplayName = "Find() with valid parameters should return 200OK status.")]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var category = await _client.GetJsonAsync<CategoryViewModel>($"api/categories/find/?id={expectedId}");

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact(DisplayName = "Find() with an invalid parameter should return 400BadRequest status.")]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>("api/categories/find/?id=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Find() with an invalid parameter should return 404NotFound status.")]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>("api/categories/find/?id=10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}