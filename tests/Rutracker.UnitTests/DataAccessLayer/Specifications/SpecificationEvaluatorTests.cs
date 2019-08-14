using System.Linq;
using Rutracker.Server.BusinessLayer.Specifications;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Specifications;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.DataAccessLayer.Specifications
{
    public class SpecificationEvaluatorTests
    {
        public static TheoryData<ISpecification<Torrent, long>, int> Data =>
            new TheoryData<ISpecification<Torrent, long>, int>
            {
                { new TorrentsFilterSpecification(null, null, null, null), 9 },
                { new TorrentsFilterSpecification("4", new[] { "5" }, 100, long.MaxValue), 2 },

                { new TorrentsFilterPaginatedSpecification(0, 5, null, null, null, null), 5 },
                { new TorrentsFilterPaginatedSpecification(4, 10, "Torrent", null, null, null), 5 },

                { new TorrentWithForumAndFilesSpecification(0), 0 },
                { new TorrentWithForumAndFilesSpecification(5), 1 }
            };

        [Theory(DisplayName = "Apply() with valid parameters should return applying specification")]
        [MemberData(nameof(Data))]
        public void SpecificationEvaluator_ValidParameters_ReturnsValidCount(ISpecification<Torrent, long> specification, int expectedCount)
        {
            // Arrange
            var torrents = DataInitializer.GeTestTorrents().AsQueryable();

            // Act
            var count = SpecificationEvaluator<Torrent, long>.Apply(torrents, specification).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}