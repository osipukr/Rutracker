using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentsFilterSpecificationTests
    {
        [Theory(DisplayName = "TorrentsFilterSpecification(params) check the search criteria")]
        [MemberData(nameof(FilterSpecificationTestData))]
        public void Specification_Filter_Should_Return_Apply_Filter(string search,
            IEnumerable<string> titles,
            long? sizeFrom,
            long? sizeTo)
        {
            // Act
            var specification = new TorrentsFilterSpecification(search, titles, sizeFrom, sizeTo);

            // Assert
            Assert.NotNull(specification.Criteria);
        }

        public static IEnumerable<object[]> FilterSpecificationTestData =>
            new[]
            {
                new object[] { null, null, null, null },
                new object[] { "search", Enumerable.Empty<string>(), long.MinValue, long.MinValue },
                new object[] { null, new [] { "10", "20" }, null, null }
            };
    }
}