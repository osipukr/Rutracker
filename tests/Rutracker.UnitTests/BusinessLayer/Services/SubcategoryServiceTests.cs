using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class SubcategoryServiceTests
    {
        private readonly ISubcategoryService _subcategoryService;
        private const int SubcategoriesCount = DataInitializer.SubcategoriesCount;

        public SubcategoryServiceTests()
        {
            var subcategoryRepository = MockInitializer.GetSubcategoryRepository();
            var categoryRepository = MockInitializer.GetCategoryRepository();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _subcategoryService = new SubcategoryService(subcategoryRepository, categoryRepository, unitOfWork);
        }

        [Fact]
        public async Task ListAsync_10_ReturnsValidSubcategories()
        {
            // Arrange
            const int expectedCount = SubcategoriesCount;

            // Act
            var subcategories = await _subcategoryService.ListAsync();

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task ListAsync_1_10_ReturnsValidSubcategories()
        {
            // Arrange
            const int categoryId = 1;
            const int expectedCount = SubcategoriesCount;

            // Act
            var subcategories = await _subcategoryService.ListAsync(categoryId);

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact]
        public async Task FindAsync_10_ReturnsValidSubcategory()
        {
            // Arrange
            const int expectedId = SubcategoriesCount;

            // Act
            var subcategory = await _subcategoryService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(subcategory);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact]
        public async Task ListAsync_NegativeNumbers_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.ListAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.FindAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_11_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.FindAsync(SubcategoriesCount + 1));

            Assert.Equal(ExceptionEventTypes.NotFound, exception.ExceptionEventType);
        }

        [Fact]
        public async Task AddAsync_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.AddAsync(null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task UpdateAsync_NegativeNumber_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.UpdateAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task DeleteAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.DeleteAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}