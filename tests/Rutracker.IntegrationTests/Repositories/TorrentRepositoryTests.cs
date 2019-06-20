using System;
using Rutracker.Core.Interfaces;
using Rutracker.Infrastructure.Data;
using Xunit;

namespace Rutracker.IntegrationTests.Repositories
{
    public class TorrentRepositoryTests : IDisposable
    {
        private readonly TorrentContext _context;
        private readonly ITorrentRepository _torrentRepository;

        public TorrentRepositoryTests()
        {
            _context = TestDatabase.GetDbContext("InMemoryDbTest");
            _torrentRepository = new TorrentRepository(_context);
        }

        [Fact(DisplayName = "ListAsync() should return 12 items")]
        public async void Repository_Get_Should_Return_12_Items()
        {
            // Arrange
            const int expectedCount = 12;

            // Act
            var count = (await _torrentRepository.ListAsync(null)).Count;

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Theory(DisplayName = "GetAsync(id) should return object with the same id")]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async void Repository_Find_Items_By_Id(long id)
        {
            // Act
            var torrentId = (await _torrentRepository.GetAsync(id)).Id;

            // Assert
            Assert.Equal(id, torrentId);
        }

        public void Dispose() => _context?.Dispose();
    }
}