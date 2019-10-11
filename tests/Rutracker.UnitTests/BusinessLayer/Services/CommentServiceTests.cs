using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class CommentServiceTests
    {
        private readonly ICommentService _commentService;
        private const int CommentsCount = DataInitializer.CommentsCount;

        public CommentServiceTests()
        {
            var commentRepository = MockInitializer.GetCommentRepository();
            var torrentRepository = MockInitializer.GetTorrentRepository();
            var likeRepository = MockInitializer.GetLikeRepository();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _commentService = new CommentService(commentRepository, torrentRepository, likeRepository, unitOfWork);
        }

        [Fact]
        public async Task ListAsync_1_9_ReturnsValidComments()
        {
            // Arrange
            const int torrentId = 1;
            const int expectedCount = CommentsCount - 1;
            const int expectedTotalCount = CommentsCount;

            // Act
            var comments = await _commentService.ListAsync(1, expectedCount, torrentId);

            // Assert
            Assert.NotNull(comments);
            Assert.Equal(expectedTotalCount, comments.Item2);
            Assert.Equal(expectedCount, comments.Item1.Count());
        }

        [Fact]
        public async Task FindAsync_10_ReturnsValidComment()
        {
            // Arrange
            const int expectedId = CommentsCount;

            // Act
            var comment = await _commentService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(expectedId, comment.Id);
        }

        [Fact]
        public async Task ListAsync_NegativeNumbers_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.ListAsync(-10, -10, -10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.FindAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_NegativeNumber_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.FindAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_11_11_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.FindAsync(CommentsCount + 1, "11"));

            Assert.Equal(ExceptionEventTypes.NotFound, exception.ExceptionEventType);
        }

        [Fact]
        public async Task AddAsync_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.AddAsync(null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task UpdateAsync_NegativeNumber_Nulls_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.UpdateAsync(-10, null, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task DeleteAsync_NegativeNumber_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _commentService.DeleteAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}