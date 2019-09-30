using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class LikeRepositoryTests
    {
        private readonly ILikeRepository _likeRepository;
        private const int LikesCount = DataInitializer.LikesCount;

        public LikeRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _likeRepository = new LikeRepository(context);
        }

        [Fact]
        public void GetAll_5_ReturnsValidLikes()
        {
            // Arrange
            const int expectedCount = LikesCount;

            // Act
            var likes = _likeRepository.GetAll();

            // Assert
            Assert.NotNull(likes);
            Assert.Equal(expectedCount, likes.Count());
        }

        [Fact]
        public void GetAll_5_ReturnsValidLikesByExpression()
        {
            // Arrange
            const int expectedId = 1;

            // Act
            var likes = _likeRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(likes);
            Assert.Single(likes);
        }

        [Fact]
        public async Task GetAsync_5_ReturnsValidLike()
        {
            // Arrange
            const int expectedId = LikesCount;

            // Act
            var like = await _likeRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(like);
            Assert.NotNull(like.UserId);
            Assert.InRange(like.CommentId, 1, int.MaxValue);
            Assert.Equal(expectedId, like.Id);
        }

        [Fact]
        public async Task GetAsync_5_ReturnsValidLikeByExpression()
        {
            // Arrange
            const int expectedId = LikesCount;

            // Act
            var like = await _likeRepository.GetAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(like);
            Assert.NotNull(like.UserId);
            Assert.InRange(like.CommentId, 1, int.MaxValue);
            Assert.Equal(expectedId, like.Id);
        }

        [Fact]
        public async Task CountAsync_5_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = LikesCount;

            // Act
            var count = await _likeRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_5_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = LikesCount;
            const int expectedCount = 1;

            // Act
            var count = await _likeRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_5_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = LikesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _likeRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_5_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = LikesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _likeRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}