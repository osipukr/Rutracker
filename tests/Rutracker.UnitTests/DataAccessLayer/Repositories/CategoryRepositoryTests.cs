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
        private const int CategoriesCount = DataInitializer.CategoriesCount;

        public CategoryRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _categoryRepository = new CategoryRepository(context);
        }

        [Fact]
        public void GetAll_10_ReturnsValidCategories()
        {
            // Arrange
            const int expectedCount = CategoriesCount;

            // Act
            var categories = _categoryRepository.GetAll();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact]
        public void GetAll_10_ReturnsValidCategoriesByExpression()
        {
            // Arrange
            const int expectedId = CategoriesCount;

            // Act
            var categories = _categoryRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(categories);
            Assert.Single(categories);
        }

        [Fact]
        public async Task GetAsync_5_ReturnsValidCategory()
        {
            // Arrange
            const int expectedId = CategoriesCount;

            // Act
            var category = await _categoryRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(category);
            Assert.NotNull(category.Name);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact]
        public async Task GetAsync_5_ReturnsValidCategoryByExpression()
        {
            // Arrange
            const int expectedId = CategoriesCount;

            // Act
            var category = await _categoryRepository.GetAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(category);
            Assert.NotNull(category.Name);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = CategoriesCount;

            // Act
            var count = await _categoryRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = CategoriesCount;
            const int expectedCount = 1;

            // Act
            var count = await _categoryRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = CategoriesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _categoryRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = CategoriesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _categoryRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}