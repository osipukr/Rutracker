using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class CategoryRepositoryTests
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _categoryRepository = new CategoryRepository(context);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid categories.")]
        public void GetAll_10_ReturnsValidCategories()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var count = _categoryRepository.GetAll().Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAll() with valid parameter should return valid categories.")]
        public void GetAll_10_ReturnsValidCategoriesByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = _categoryRepository.GetAll(x => x.Id == 10).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid category.")]
        public async Task GetAsync_5_ReturnsValidCategory()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var categoryId = (await _categoryRepository.GetAsync(expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, categoryId);
        }

        [Fact(DisplayName = "GetAsync() with valid parameter should return valid category.")]
        public async Task GetAsync_5_ReturnsValidCategoryByExpression()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var categoryId = (await _categoryRepository.GetAsync(x => x.Id == expectedId)).Id;

            // Assert
            Assert.Equal(expectedId, categoryId);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var count = await _categoryRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "CountAsync() with valid parameters should return the number of items.")]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedCount = 1;

            // Act
            var count = await _categoryRepository.CountAsync(x => x.Id == 10);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _categoryRepository.ExistAsync(10);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "ExistAsync() with valid parameters should return whether the object exists.")]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const bool expected = true;

            // Act
            var result = await _categoryRepository.ExistAsync(x => x.Id == 10);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}