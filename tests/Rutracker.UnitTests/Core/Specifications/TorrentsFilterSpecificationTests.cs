using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Core.Specifications
{
    public class TorrentsFilterSpecificationTests
    {
        public static IEnumerable<object[]> FilterSpecificationTestCases =>
            new[]
            {
                new object[] { null, null, null, null },
                new object[] { "search", Enumerable.Empty<string>(), long.MinValue, long.MinValue },
                new object[] { null, new [] { "10", "20" }, null, null }
            };

        [Theory(DisplayName = "TorrentsFilterSpecification() with valid parameters should return valid specification")]
        [MemberData(nameof(FilterSpecificationTestCases))]
        public void TorrentsFilterSpecification_ValidParameters_ReturnsValidSpecification(
            string search,
            IEnumerable<string> titles,
            long? sizeFrom,
            long? sizeTo)
        {
            // Act
            var specification = new TorrentsFilterSpecification(search, titles, sizeFrom, sizeTo);

            // Assert
            Assert.NotNull(specification.Criteria);
        }
    }
}