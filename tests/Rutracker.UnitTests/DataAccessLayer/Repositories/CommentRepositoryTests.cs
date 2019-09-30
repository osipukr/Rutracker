using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class CommentRepositoryTests
    {
        private readonly ICommentRepository _commentRepository;
        private const int CommentsCount = DataInitializer.CommentsCount;

        public CommentRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _commentRepository = new CommentRepository(context);
        }

        [Fact]
        public void GetAll_10_ReturnsValidComments()
        {
            // Arrange
            const int expectedCount = CommentsCount;

            // Act
            var comments = _commentRepository.GetAll();

            // Assert
            Assert.NotNull(comments);
            Assert.Equal(expectedCount, comments.Count());
        }

        [Fact]
        public void GetAll_10_ReturnsValidCommentsByExpression()
        {
            // Arrange
            const int expectedId = CommentsCount;

            // Act
            var comments = _commentRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(comments);
            Assert.Single(comments);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidComment()
        {
            // Arrange
            const int expectedId = CommentsCount;

            // Act
            var comment = await _commentRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(comment);
            Assert.NotNull(comment.Text);
            Assert.NotNull(comment.UserId);
            Assert.InRange(comment.TorrentId, 1, int.MaxValue);
            Assert.Equal(expectedId, comment.Id);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidCommentByExpression()
        {
            // Arrange
            const int expectedId = CommentsCount;

            // Act
            var comment = await _commentRepository.GetAsync(x => x.Id == expectedId);

            // Assert
            Assert.NotNull(comment);
            Assert.NotNull(comment.Text);
            Assert.NotNull(comment.UserId);
            Assert.InRange(comment.TorrentId, 1, int.MaxValue);
            Assert.Equal(expectedId, comment.Id);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = CommentsCount;

            // Act
            var count = await _commentRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = CommentsCount;
            const int expectedCount = 1;

            // Act
            var count = await _commentRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = CommentsCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _commentRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = CommentsCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _commentRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}