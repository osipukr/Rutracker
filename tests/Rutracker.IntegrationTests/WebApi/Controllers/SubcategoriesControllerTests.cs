using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models.ViewModels.Subcategory;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class SubcategoriesControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public SubcategoriesControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact]
        public async Task List_30_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = WebApiContextSeed.SubcategoryMaxCount;

            // Act
            var subcategories = await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>("api/subcategories");

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task Search_1_ReturnsStatus200OK()
        {
            const int categoryId = 1;
            const int expectedCount = WebApiContextSeed.SubcategoryMaxCount;

            // Act
            var subcategories = await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(
                $"api/subcategories/search?categoryId={categoryId}");

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var subcategory = await _client.GetJsonAsync<SubcategoryViewModel>($"api/subcategories/{expectedId}");

            // Assert
            Assert.NotNull(subcategory);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact]
        public async Task Search_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>("api/subcategories/search?categoryId=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>("api/subcategories/-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>("api/subcategories/10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}