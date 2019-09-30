using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.IntegrationTests.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Category;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class CategoriesControllerTests : BaseTestController
    {
        private const int CategoriesCount = WebApiContextSeed.CategoryMaxCount;
        private const string ListPath = "api/categories";
        private const string FindPath = "api/categories/{0}";

        public CategoriesControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task List_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = CategoriesCount;

            // Act
            var categories = await _client.GetJsonAsync<IEnumerable<CategoryViewModel>>(ListPath);

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact]
        public async Task Find_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = CategoriesCount;

            // Act
            var category = await _client.GetJsonAsync<CategoryViewModel>(string.Format(FindPath, expectedId));

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>(string.Format(FindPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_21_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CategoryViewModel>(string.Format(FindPath, CategoriesCount + 1)));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}