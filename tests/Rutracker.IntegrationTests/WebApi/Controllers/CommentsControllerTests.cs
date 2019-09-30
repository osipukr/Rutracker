using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Rutracker.IntegrationTests.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers
{
    public class CommentsControllerTests : BaseTestController
    {
        private const int CommentsCount = WebApiContextSeed.CommentMaxCount;
        private const int TorrentsCount = WebApiContextSeed.TorrentMaxCount;
        private const string SearchPath = "api/comments/search?page={0}&pageSize={1}&torrentId={2}";
        private const string FindPath = "api/comments/{0}";

        public CommentsControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Search_1_5_20_ReturnsStatus200OK()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int torrentId = TorrentsCount;
            const int expectedCount = pageSize;
            const int expectedTotalItems = CommentsCount;

            // Act
            var comments = await _client.GetJsonAsync<PaginationResult<CommentViewModel>>(
                string.Format(SearchPath, page, pageSize, torrentId));

            // Assert
            Assert.NotNull(comments);
            Assert.NotNull(comments.Items);
            Assert.Equal(page, comments.Page);
            Assert.Equal(pageSize, comments.PageSize);
            Assert.Equal(expectedTotalItems, comments.TotalItems);
            Assert.Equal(expectedCount, comments.Items.Count());
        }

        [Fact]
        public async Task Find_20_ReturnsStatus200OK()
        {
            // Arrange
            const int expectedId = CommentsCount;

            // Act
            var comment = await _client.GetJsonAsync<CommentViewModel>(string.Format(FindPath, expectedId));

            // Assert
            Assert.NotNull(comment);
            Assert.NotNull(comment.Text);
            Assert.Equal(expectedId, comment.Id);
        }

        [Fact]
        public async Task Search_NegativeNumbers_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<PaginationResult<CommentViewModel>>(
                    string.Format(SearchPath, -10, -10, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_NegativeNumber_ReturnsStatus400BadRequest()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CommentViewModel>(string.Format(FindPath, -10)));

            Assert.Contains(StatusCodes.Status400BadRequest.ToString(), exception.Message);
        }

        [Fact]
        public async Task Find_1000_ReturnsStatus404NotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<CommentViewModel>(string.Format(FindPath, CommentsCount + 1)));

            Assert.Contains(StatusCodes.Status404NotFound.ToString(), exception.Message);
        }
    }
}