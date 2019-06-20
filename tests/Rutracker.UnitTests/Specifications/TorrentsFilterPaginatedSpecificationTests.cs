using System.Linq;
using Rutracker.Core.Specifications;
using Xunit;

namespace Rutracker.UnitTests.Specifications
{
    public class TorrentsFilterPaginatedSpecificationTests
    {
        [Theory(DisplayName = "TorrentsFilterPaginatedSpecification() check the number of items on the first page")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public void Expected_Items_Count_On_First_Page(int count)
        {
            // Arrange
            var specification = new TorrentsFilterPaginatedSpecification(0, count,
                default,
                default,
                default,
                default);

            var torrents = DataSeed.GetTestTorrentItems().AsQueryable();

            // Act
            var result = torrents.Where(specification.Criteria)
                .Skip(specification.Skip)
                .Take(specification.Take)
                .Count();

            // Assert
            Assert.Equal(count, result);
        }

        [Theory(DisplayName = "TorrentsFilterPaginatedSpecification() check the number of items on the N-page")]
        [InlineData(3, 10, 0)]
        [InlineData(4, 6, 2)]
        [InlineData(1, 5, 5)]
        [InlineData(3, 8, 4)]
        [InlineData(1, 10, 10)]
        [InlineData(1, 20, 20)]
        
        public void Expected_Items_Count_Per_Page(int page, int count, int expectedCount)
        {
            // Arrange
            var specification = new TorrentsFilterPaginatedSpecification((page - 1) * count, count,
                default,
                default,
                default,
                default);

            var torrents = DataSeed.GetTestTorrentItems().AsQueryable();

            // Act
            var result = torrents.Where(specification.Criteria)
                .Skip(specification.Skip)
                .Take(specification.Take)
                .Count();

            // Assert
            Assert.Equal(expectedCount, result);
        }
    }
}