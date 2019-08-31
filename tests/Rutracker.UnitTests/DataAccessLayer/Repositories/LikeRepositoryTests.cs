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

        public LikeRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _likeRepository = new LikeRepository(context);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid likes.")]
        public void GetAll_5_ReturnsValidLikes()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var count = _likeRepository.GetAll().Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid likes.")]
        public void GetAll_5_ReturnsValidLikesByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = _likeRepository.GetAll(x => x.Id == 5).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid like.")]
        public async Task GetAsync_5_ReturnsValidLike()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var categoryId = (await _likeRepository.GetAsync(expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, categoryId);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid like.")]
        public async Task GetAsync_5_ReturnsValidLikeByExpression()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var categoryId = (await _likeRepository.GetAsync(x => x.Id == expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, categoryId);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_5_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var count = await _likeRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_5_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = await _likeRepository.CountAsync(x => x.Id == 5);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_5_ReturnsValidIsExist()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _likeRepository.ExistAsync(5);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_5_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _likeRepository.ExistAsync(x => x.Id == 5);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}