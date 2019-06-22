using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentWithForumAndFilesSpecificationTests
    {
        [Theory(DisplayName = "TorrentWithForumAndFilesSpecification(id) check the criteria for linking entities")]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void Specification_ForumWithFiles_Should_Return_Related_Entity(long id)
        {
            // Arrange
            const int expectedCount = 2;
            var specification = new TorrentWithForumAndFilesSpecification(id);

            // Act
            var count = specification.Includes.Count;

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}