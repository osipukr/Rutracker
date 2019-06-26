using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Infrastructure.Data;
using Xunit;

namespace Rutracker.UnitTests.Infrastructure.Specifications
{
    public class SpecificationEvaluatorTests
    {
        [Theory(DisplayName = "Apply(query,spec) should return the correct number of items after applying the specification")]
        [MemberData(nameof(EvaluatorTestCases))]
        public void Evaluator_Should_Correctly_Apply_The_Specification(ISpecification<Torrent, long> specification, int expectedCount)
        {
            // Arrange
            var torrents = TorrentTestData.AsQueryable();

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

        private static IEnumerable<Torrent> TorrentTestData =>
            new[]
            {
                new Torrent { Id = 1, Title = "Torrent Titles 1", ForumId = 1, Size = 100 },
                new Torrent { Id = 2, Title = "Torrent Titles 12", ForumId = 1, Size = 100 },
                new Torrent { Id = 3, Title = "Torrent Titles 123", ForumId = 1, Size = 1000 },
                new Torrent { Id = 4, Title = "Torrent Titles 1234", ForumId = 2, Size = 1000 },
                new Torrent { Id = 5, Title = "Torrent Titles 12345", ForumId = 2, Size = 10000 },
                new Torrent { Id = 6, Title = "Torrent Titles 123456", ForumId = 3, Size = 10000 },
                new Torrent { Id = 7, Title = "Torrent Titles 1234567", ForumId = 4, Size = 100000 },
                new Torrent { Id = 8, Title = "Torrent Titles 12345678", ForumId = 5, Size = 100000 },
                new Torrent { Id = 9, Title = "Torrent Titles 123456789", ForumId = 5, Size = 100000 }
            };
    }
}