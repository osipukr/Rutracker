using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class CommentServiceTests
    {
        private readonly ICommentService _commentService;

        public CommentServiceTests()
        {
            var commentRepository = MockInitializer.GetCommentRepository();
            var torrentRepository = MockInitializer.GetTorrentRepository();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _commentService = new CommentService(commentRepository, torrentRepository, unitOfWork);
        }

        [Fact(DisplayName = "ListAsync() with valid parameters should return the comments list.")]
        public async Task ListAsync_1_10_ReturnsValidComments()
        {
            // Arrange
            const int torrentId = 1;
            const int expectedCount = 3;

            // Act
            var comments = await _commentService.ListAsync(torrentId, expectedCount);

            // Assert
            Assert.NotNull(comments);
            Assert.Equal(expectedCount, comments.Count());
        }

        [Fact(DisplayName = "FindAsync() with valid parameter should return the comment with id.")]
        public async Task FindAsync_5_ReturnsValidComment()
        {
            // Arrange
            const string expectedUserId = "1";
            const int expectedId = 1;

            // Act
            var comment = await _commentService.FindAsync(expectedId, expectedUserId);

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(expectedId, comment.Id);
            Assert.Equal(expectedUserId, comment.UserId);
        }

        [Fact(DisplayName = "AddAsync() with valid parameter should return the comment.")]
        public async Task AddAsync_NewEntity_ReturnsValidComment()
        {
            // Arrange
            var comment = new Comment
            {
                Text = "Comment text",
                TorrentId = 1,
                UserId = "1"
            };

            // Act
            var result = await _commentService.AddAsync(comment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Text, result.Text);
            Assert.Equal(comment.TorrentId, result.TorrentId);
            Assert.Equal(comment.UserId, result.UserId);
        }

        [Fact(DisplayName = "UpdateAsync() with valid parameter should return the comment.")]
        public async Task UpdateAsync_1_1_ReturnsValidComment()
        {
            // Arrange
            const int commentId = 1;
            const string userId = "1";
            var comment = new Comment
            {
                Text = "New comment text"
            };

            // Act
            var result = await _commentService.UpdateAsync(commentId, userId, comment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commentId, result.Id);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(comment.Text, result.Text);
            Assert.True(result.IsModified);
            Assert.NotNull(result.LastUpdatedAt);
        }

        [Fact(DisplayName = "DeleteAsync() with valid parameter should return the comment.")]
        public async Task DeleteAsync_1_1_ReturnsValidComment()
        {
            // Arrange
            const int commentId = 1;
            const string userId = "1";

            // Act
            var comment = await _commentService.DeleteAsync(commentId, userId);

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(commentId, comment.Id);
            Assert.Equal(userId, comment.UserId);
        }

        [Fact(DisplayName = "ListAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task ListAsync_NegativeNumbers_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.ListAsync(-10, -10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_NegativeNumber_Null_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.FindAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_1000_1000_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.FindAsync(1000, "1000"));

            Assert.Equal(ExceptionEventTypes.NotFound, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "AddAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task AddAsync_Null_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.AddAsync(null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "UpdateAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task UpdateAsync_NegativeNumber_Null_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.UpdateAsync(-10, null, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "DeleteAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task DeleteAsync_NegativeNumber_Null_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.DeleteAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}