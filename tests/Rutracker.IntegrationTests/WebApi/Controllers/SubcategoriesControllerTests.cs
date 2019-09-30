using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.IntegrationTests.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Subcategory;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class SubcategoriesControllerTests : BaseTestController
    {
        private const int CategoriesCount = WebApiContextSeed.CategoryMaxCount;
        private const int SubcategoriesCount = WebApiContextSeed.SubcategoryMaxCount;
        private const string ListPath = "api/subcategories";
        private const string SearchPath = "api/subcategories/search?categoryId={0}";
        private const string FindPath = "api/subcategories/{0}";

        public SubcategoriesControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task List_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCount = SubcategoriesCount;

            // Act
            var subcategories = await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(ListPath);

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task Search_20_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedCategoryId = CategoriesCount;
            const int expectedCount = SubcategoriesCount;

            // Act
            var subcategories = await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(
                string.Format(SearchPath, expectedCategoryId));

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task Find_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;

            // Act
            var subcategory = await _client.GetJsonAsync<SubcategoryViewModel>(string.Format(FindPath, expectedId));

            // Assert
            Assert.NotNull(subcategory);
            Assert.NotNull(subcategory.Name);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact]
        public async Task Search_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(
                    string.Format(SearchPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>(string.Format(FindPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_31_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>(string.Format(FindPath, SubcategoriesCount + 1)));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}