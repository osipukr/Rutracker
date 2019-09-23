using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class LikeServiceTests
    {
        private readonly ILikeService _likeService;

        public LikeServiceTests()
        {
            var likeRepository = MockInitializer.GetLikeRepository();
            var commentRepository = MockInitializer.GetCommentRepository();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _likeService = new LikeService(likeRepository, commentRepository, unitOfWork);
        }

        [Fact(DisplayName = "LikeCommentAsync() with valid parameters should return the like.")]
        public async Task LikeCommentAsync_1_1_ReturnsValidLike()
        {
            // Arrange
            const int commentId = 1;
            const string userId = "1";

            // Act
            var like = await _likeService.LikeCommentAsync(commentId, userId);

            // Assert
            Assert.NotNull(like);
            Assert.Equal(commentId, like.CommentId);
            Assert.Equal(userId, like.UserId);
        }

        [Fact(DisplayName = "LikeCommentAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task LikeCommentAsync_NegativeNumber_Null_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _likeService.LikeCommentAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "LikeCommentAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task LikeCommentAsync_1000_1_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _likeService.LikeCommentAsync(1000, "1"));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}