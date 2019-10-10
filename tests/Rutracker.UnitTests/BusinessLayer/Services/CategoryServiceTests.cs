using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Services;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.BusinessLayer.Services
{
    public class CategoryServiceTests
    {
        private readonly ICategoryService _categoryService;
        private const int CategoriesCount = DataInitializer.CategoriesCount;

        public CategoryServiceTests()
        {
            var categoryRepository = MockInitializer.GetCategoryRepository();
            var unitOfWork = MockInitializer.GetUnitOfWork();

            _categoryService = new CategoryService(categoryRepository, unitOfWork);
        }

        [Fact]
        public async Task ListAsync_10_ReturnsValidCategories()
        {
            // Arrange
            const int expectedCount = CategoriesCount;

            // Act
            var categories = await _categoryService.ListAsync();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact]
        public async Task FindAsync_10_ReturnsValidCategory()
        {
            // Arrange
            const int expectedId = CategoriesCount;

            // Act
            var category = await _categoryService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact]
        public async Task FindAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.FindAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task FindAsync_11_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.FindAsync(CategoriesCount + 1));

            Assert.Equal(ExceptionEventTypes.NotFound, exception.ExceptionEventType);
        }

        [Fact]
        public async Task AddAsync_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.AddAsync(null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task UpdateAsync_NegativeNumber_Null_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.UpdateAsync(-10, null));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact]
        public async Task DeleteAsync_NegativeNumber_ThrowRutrackerException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.DeleteAsync(-10));

            Assert.Equal(ExceptionEventTypes.NotValidParameters, exception.ExceptionEventType);
        }
    }
}