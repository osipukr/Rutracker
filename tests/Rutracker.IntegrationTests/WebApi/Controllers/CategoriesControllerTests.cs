using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models.ViewModels.Category;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class CategoriesControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact]
        public async Task List_30_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = WebApiContextSeed.CategoryMaxCount;

            // Act
            var categories = await _client.GetJsonAsync<IEnumerable<CategoryViewModel>>("api/categories");

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var category = await _client.GetJsonAsync<CategoryViewModel>($"api/categories/{expectedId}");

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>("api/categories/-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_10000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>("api/categories/10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}