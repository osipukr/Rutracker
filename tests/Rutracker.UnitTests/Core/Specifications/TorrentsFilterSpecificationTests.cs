using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Core.Specifications
{
    public class TorrentsFilterSpecificationTests
    {
        public static TheoryData<string, IEnumerable<string>, long?, long?> Data =>
            new TheoryData<string, IEnumerable<string>, long?, long?>
            {
                { null, null, null, null },
                { "search", Enumerable.Empty<string>(), long.MinValue, long.MinValue },
                { null, new [] { "10", "20" }, null, null }
            };

        [Theory(DisplayName = "TorrentsFilterSpecification() with valid parameters should return valid specification")]
        [MemberData(nameof(Data))]
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