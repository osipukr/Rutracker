using System.Collections.Generic;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentsFilterPaginatedSpecificationTests
    {
        [Theory(DisplayName = "TorrentsFilterPaginatedSpecification(params) check the pagination criteria")]
        [MemberData(nameof(FilterPaginatedSpecificationTestData))]
        public void Specification_FilterPaginated_Should_Return_Apply_Filer(int skip, int take,
            string search,
            IEnumerable<string> titles,
            long? sizeFrom,
            long? sizeTo)
        {
            // Arrange
            var specification = new TorrentsFilterPaginatedSpecification(skip, take,
                search,
                titles,
                sizeFrom,
                sizeTo);
            
            // Act & Assert
            Assert.Equal(skip, specification.Skip);
            Assert.Equal(take, specification.Take);
            Assert.True(specification.IsPagingEnabled);
            Assert.NotNull(specification.Criteria);
        }

        public static IEnumerable<object[]> FilterPaginatedSpecificationTestData =>
            new[]
            {
                new object[] { 0, 0, default, default, default, default },
                new object[] { 10, 20, "search", default, default, default },
                new object[] { 20, 30, default, new [] { "20", "30" }, long.MinValue, long.MaxValue / 2 },
                new object[] { 30, 40, "search", new string[] { }, long.MinValue, long.MaxValue }
            };
    }
}