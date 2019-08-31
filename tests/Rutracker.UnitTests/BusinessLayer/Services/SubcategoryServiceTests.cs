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

        public SubcategoryServiceTests()
        {
            var repository = MockInitializer.GetSubcategoryRepository();

            _subcategoryService = new SubcategoryService(repository);
        }

        [Fact(DisplayName = "ListAsync() with valid parameters should return the subcategories list.")]
        public async Task ListAsync_5_ReturnsValidSubcategories()
        {
            // Arrange
            const int categoryId = 5;
            const int expectedCount = 3;

            // Act
            var subcategories = await _subcategoryService.ListAsync(categoryId);

            // Assert
            Assert.NotNull(subcategories);
            Assert.Equal(expectedCount, subcategories.Count());
        }

        [Fact(DisplayName = "FindAsync() with valid parameter should return the subcategories with id.")]
        public async Task FindAsync_5_ReturnsValidSubcategory()
        {
            // Arrange
            const int expectedId = 5;

            // Act
            var subcategory = await _subcategoryService.FindAsync(expectedId);

            // Assert
            Assert.NotNull(subcategory);
            Assert.Equal(expectedId, subcategory.Id);
        }

        [Fact(DisplayName = "ListAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task ListAsync_NegativeNumbers_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.ListAsync(-10));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_NegativeNumber_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.FindAsync(-10));

            Assert.Equal(ExceptionEventType.NotValidParameters, exception.ExceptionEventType);
        }

        [Fact(DisplayName = "FindAsync() with an invalid parameters should throw RutrackerException.")]
        public async Task FindAsync_1000_ThrowTorrentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RutrackerException>(async () =>
                await _subcategoryService.FindAsync(1000));

            Assert.Equal(ExceptionEventType.NotFound, exception.ExceptionEventType);
        }
    }
}