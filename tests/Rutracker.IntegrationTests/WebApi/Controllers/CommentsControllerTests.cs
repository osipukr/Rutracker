using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class CommentsControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public CommentsControllerTests(WebApiFactory factory) => _client = factory.CreateClient();

        [Fact]
        public async Task Search_1_5_1_ReturnsStatus200OK()
        {
            const int page = 1;
            const int pageSize = 5;
            const int torrentId = 1;
            const int expectedCount = 5;

            // Act
            var comments = await _client.GetJsonAsync<PaginationResult<CommentViewModel>>(
                $"api/comments/search?page={page}&pageSize={pageSize}&torrentId={torrentId}");

            // Assert
            Assert.NotNull(comments);
            Assert.NotNull(comments.Items);
            Assert.Equal(page, comments.Page);
            Assert.Equal(pageSize, comments.PageSize);
            Assert.Equal(expectedCount, comments.Items.Count());
        }

        [Fact]
        public async Task Find_5_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var comment = await _client.GetJsonAsync<CommentViewModel>($"api/comments/{expectedId}");

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(expectedId, comment.Id);
        }

        [Fact]
        public async Task Search_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<PaginationResult<CommentViewModel>>("api/comments/search?page=-10&pageSize=-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CommentViewModel>("api/comments/-10"));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CommentViewModel>("api/comments/10000"));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}