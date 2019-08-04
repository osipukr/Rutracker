using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Core.Specifications
{
    public class TorrentWithForumAndFilesSpecificationTests
    {
        [Theory(DisplayName = "TorrentWithForumAndFilesSpecification() with valid parameter should return valid specification")]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void TorrentWithForumAndFilesSpecification_ValidParameter_ReturnsValidSpecification(long id)
        {
            // Arrange
            const int expectedIncludeCount = 2;

            // Act
            var specification = new TorrentWithForumAndFilesSpecification(id);

            // Assert
            Assert.NotNull(specification.Includes);
            Assert.Equal(expectedIncludeCount, specification.Includes.Count);
        }
    }
}