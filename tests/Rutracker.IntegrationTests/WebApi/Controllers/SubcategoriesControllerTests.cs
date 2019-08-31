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
    public class SubcategoriesControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public SubcategoriesControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact(DisplayName = "List() with valid parameters should return 200OK status")]
        public async Task List_1_ReturnsStatus200OK()
        {
            // Act
            var subcategories = await _client.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(
                "api/subcategories/?categoryId=1");

            // Assert
            Assert.NotNull(subcategories);
        }

        [Fact(DisplayName = "Find() with valid parameters should return 200OK status")]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var subcategory = await _client.GetJsonAsync<SubcategoryViewModel>(
                $"api/subcategories/find/?id={expectedId}");

            // Assert
            Assert.NotNull(subcategory);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact(DisplayName = "Find() with an invalid parameter should return 400BadRequest status")]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>("api/subcategories/find/?id=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact(DisplayName = "Find() with an invalid parameter should return 404NotFound status")]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<SubcategoryViewModel>("api/subcategories/find/?id=10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}