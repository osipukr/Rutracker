using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Infrastructure.Data;
using Rutracker.UnitTests.TestData;
using Xunit;

namespace Rutracker.UnitTests.Infrastructure.Specifications
{
    public class SpecificationEvaluatorTests : IDisposable
    {
        private readonly TorrentContext _context;

        public SpecificationEvaluatorTests()
        {
            _context = TestDatabase.GetContext("SpecificationEvaluatorInMemoryTestDb");
        }

        [Theory(DisplayName = "Apply(query,spec) should return the correct number of items after applying the specification")]
        [MemberData(nameof(EvaluatorTestData))]
        public void Evaluator_Should_Correctly_Apply_The_Specification(ISpecification<Torrent, long> specification, int expectedCount)
        {
            // Arrange
            var torrents = _context.Torrents.AsQueryable();

            // Act
            var count = SpecificationEvaluator<Torrent, long>.Apply(torrents, specification).Count();

            // Assert
            Assert.Equal(expectedCount, count);
        }

        public static IEnumerable<object[]> EvaluatorTestData =>
            new []
            {
                new object[] { new TorrentsFilterSpecification(null, null, null, null), 12 },
                new object[] { new TorrentsFilterSpecification("Title 8", new[] { "5" }, 100000, long.MaxValue), 2},

                new object[] { new TorrentsFilterPaginatedSpecification(0, 10, null, null, null, null), 10,  },
                new object[] { new TorrentsFilterPaginatedSpecification(5, 20, "Torrent", null, null, null), 7 },

                new object[] { new TorrentWithForumAndFilesSpecification(0), 0 },
                new object[] { new TorrentWithForumAndFilesSpecification(10), 1 }
            };

        public void Dispose() => _context?.Dispose();
    }
}