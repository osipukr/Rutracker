using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Core.Specifications
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
            const int expectedIncludeCount = 2;

            // Act
            var specification = new TorrentWithForumAndFilesSpecification(id);

            // Assert
            Assert.NotNull(specification.Includes);
            Assert.Equal(expectedIncludeCount, specification.Includes.Count);
        }
    }
}