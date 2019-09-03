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

        public CategoryServiceTests()
        {
            var repository = MockInitializer.GetCategoryRepository();

            _categoryService = new CategoryService(repository);
        }

        [Fact(DisplayName = "ListAsync() with valid parameters should return the categories list.")]
        public async Task ListAsync_10_ReturnsValidCategories()
        {
            // Arrange
            const int expectedCount = 10;

            // Act
            var categories = await _categoryService.ListAsync();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(expectedCount, categories.Count());
        }

        [Fact(DisplayName = "FindAsync() with valid parameter should return the category with id.")]
        public async Task FindAsync_5_ReturnsValidCategory()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var category = await _categoryService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(expectedId, category.Id);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_NegativeNumber_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.FindAsync(-10));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_1000_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _categoryService.FindAsync(1000));

            Assert.Equal(ExceptionEventType.NotFound, exception.ExceptionEventType);
        }
    }
}