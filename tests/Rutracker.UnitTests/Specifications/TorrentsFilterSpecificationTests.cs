using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentsFilterSpecificationTests
    {
        [Theory(DisplayName = "TorrentsFilterSpecification() check of criteria")]
        [InlineData(null, null, null, null, 20)]
        [InlineData("", new [] {"70"}, null, null, 6)]
        [InlineData("34", null, null, null, 6)]
        [InlineData("abc", null, 100, null, 6)]
        [InlineData("0123456789", new[] { "70" }, null, 5000, 0)]
        [InlineData("0", null, 800, 2000, 2)]
        public void Matches_Expected_Number_Of_Items(string search, IEnumerable<string> titles, long? sizeFrom, long? sizeTo, int expectedCount)
        {
            // Arrange
            var specification = new TorrentsFilterSpecification(search, titles, sizeFrom, sizeTo);
            var torrents = DataSeed.GetTestTorrentItems().AsQueryable();

            // Act
            var result = torrents.Where(specification.Criteria).Count();

            // Assert
            Assert.Equal(expectedCount, result);
        }
    }
}