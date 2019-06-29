using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Infrastructure.Data;
using Rutracker.UnitTests.Setup;
using Xunit;

namespace Rutracker.UnitTests.Infrastructure.Specifications
{
    public class SpecificationEvaluatorTests
    {
        [Theory(DisplayName =
            "Apply(query,spec) should return the correct number of items after applying the specification")]
        [MemberData(nameof(EvaluatorTestCases))]
        public void Evaluator_Should_Correctly_Apply_The_Specification(ISpecification<Torrent, long> specification,
            int expectedCount)
        {
            // Arrange
            var torrents = DataInitializer.GeTorrents().AsQueryable();

            // Act
            var count = SpecificationEvaluator<Torrent, long>.Apply(torrents, specification).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        public static IEnumerable<object[]> EvaluatorTestCases =>
            new[]
            {
                new object[] { new TorrentsFilterSpecification(null, null, null, null), 9 },
                new object[] { new TorrentsFilterSpecification("4", new[] { "5" }, 100, long.MaxValue), 2},

                new object[] { new TorrentsFilterPaginatedSpecification(0, 5, null, null, null, null), 5,  },
                new object[] { new TorrentsFilterPaginatedSpecification(4, 10, "Torrent", null, null, null), 5 },

                new object[] { new TorrentWithForumAndFilesSpecification(0), 0 },
                new object[] { new TorrentWithForumAndFilesSpecification(5), 1 }
            };
    }
}