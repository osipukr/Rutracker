using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Repositories
{
    public class SubcategoryRepositoryTests
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private const int SubcategoriesCount = DataInitializer.SubcategoriesCount;

        public SubcategoryRepositoryTests()
        {
            var context = MockInitializer.GetRutrackerContext();

            _subcategoryRepository = new SubcategoryRepository(context);
        }

        [Fact]
        public void GetAll_10_ReturnsValidSubcategories()
        {
            // Arrange
            const int expectedCount = SubcategoriesCount;

            // Act
            var subcategories = _subcategoryRepository.GetAll();

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public void GetAll_10_ReturnsValidSubcategoriesByExpression()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;

            // Act
            var subcategories = _subcategoryRepository.GetAll(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(subcategories);
            Assert.Single(subcategories);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidSubcategory()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;

            // Act
            var subcategory = await _subcategoryRepository.GetAsync(expectedId);

            // Assert
            Assert.NotNull(subcategory);
            Assert.NotNull(subcategory.Name);
            Assert.InRange(subcategory.CategoryId, 1, int.MaxValue);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact]
        public async Task GetAsync_10_ReturnsValidSubcategoryByExpression()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;

            // Act
            var subcategory = await _subcategoryRepository.GetAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.NotNull(subcategory);
            Assert.NotNull(subcategory.Name);
            Assert.InRange(subcategory.CategoryId, 1, int.MaxValue);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCount()
        {
            // Arrange
            const int expectedCount = SubcategoriesCount;

            // Act
            var count = await _subcategoryRepository.CountAsync();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task CountAsync_10_ReturnsValidCountByExpression()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;
            const int expectedCount = 1;

            // Act
            var count = await _subcategoryRepository.CountAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExist()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _subcategoryRepository.ExistAsync(expectedId);

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }

        [Fact]
        public async Task ExistAsync_10_ReturnsValidIsExistByExpression()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;
            const bool expectedCondition = true;

            // Act
            var isExist = await _subcategoryRepository.ExistAsync(x => x.Id.Equals(expectedId));

            // Assert
            Assert.Equal(expectedCondition, isExist);
        }
    }
}