using System.Collections.Generic;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Core.Specifications
{
    public class TorrentWithForumAndFilesSpecificationTests
    {
        public static IEnumerable<object[]> TorrentWithForumAndFilesSpecificationTestCases =>
            new[]
            {
                new object[] { 1 },
                new object[] { 5 },
                new object[] { 10 }
            };

        [Theory(DisplayName = "TorrentWithForumAndFilesSpecification() with valid parameter should return valid specification")]
        [MemberData(nameof(TorrentWithForumAndFilesSpecificationTestCases))]
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