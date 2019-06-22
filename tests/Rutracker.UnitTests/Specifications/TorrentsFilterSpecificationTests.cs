using System.Collections.Generic;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentsFilterSpecificationTests
    {
        [Theory(DisplayName = "TorrentsFilterSpecification(params) check the search criteria")]
        [MemberData(nameof(FilterSpecificationTestData))]
        public void Specification_Filter_Should_Return_Apply_Filter(string search, IEnumerable<string> titles, long? sizeFrom, long? sizeTo)
        {
            // Arrange
            var specification = new TorrentsFilterSpecification(search, titles, sizeFrom, sizeTo);

            // Act & Assert
            Assert.NotNull(specification.Criteria);
        }

        public static IEnumerable<object[]> FilterSpecificationTestData =>
            new[]
            {
                new object[] { default, default, default, default },
                new object[] { "search", default, long.MinValue, long.MinValue },
                new object[] { default, new [] { "10", "20" }, default, default }
            };
    }
}