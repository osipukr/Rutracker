using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Infrastructure.Data;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Mapping;
using Rutracker.Server.Services;
using Rutracker.Shared.ViewModels;
using Rutracker.UnitTests.TestData;
using Xunit;

namespace Rutracker.UnitTests.Server.Services
{
    public class TorrentViewModelServiceTests : IDisposable
    {
        private readonly TorrentContext _context;
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentViewModelServiceTests()
        {
            _context = TestDatabase.GetContext("TorrentServiceInMemoryTestDb");

            var repository = new TorrentRepository(_context);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfiles(new Profile[]
            {
                new TorrentViewModelProfile(),
                new TorrentDetailsViewModelProfile(),
                new ForumViewModelProfile(),
                new FileViewModelProfile()
            })));

            _torrentViewModelService = new TorrentViewModelService(repository, mapper, cache);
        }

        [Theory(DisplayName = "GetTorrentsIndexAsync(page,size,filter) should return the torrents page")]
        [MemberData(nameof(TorrentsIndexTestData))]
        public async void Service_GetTorrentsIndexAsync_Should_Return_Torrents_Page(int page, int pageSize,
            FiltrationViewModel filter,
            int expectedCount)
        {
            // Act
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentItems);
            Assert.NotNull(result.PaginationModel);

            Assert.Equal(expectedCount, result.TorrentItems.Length);
            Assert.Equal(page, result.PaginationModel.CurrentPage);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);

            Assert.InRange(result.PaginationModel.TotalPages, int.MinValue, int.MaxValue);
            Assert.InRange(result.PaginationModel.TotalItems, int.MinValue, int.MaxValue);
        }

        [Theory(DisplayName = "GetTorrentIndexAsync(id) should return a torrent page for a specific id")]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(12)]
        public async void Service_GetTorrentIndexAsync_Should_Return_Torrent_Details_Page(long id)
        {
            // Act
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.TorrentDetailsItem);
            Assert.NotNull(result.TorrentDetailsItem.Forum);

            Assert.Equal(id, result.TorrentDetailsItem.Id);
        }

        [Theory(DisplayName = "GetTitlesAsync(count) should return a count of forum titles")]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        [InlineData(10, 8)]
        public async void Service_GetTitlesAsync_Should_Return_N_Forum_Titles(int count, int expectedCount)
        {
            // Act
            var result = await _torrentViewModelService.GetTitlesAsync(count);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(expectedCount, result.Length);

            Assert.DoesNotContain(result, x => string.IsNullOrEmpty(x.Id));
            Assert.DoesNotContain(result, x => string.IsNullOrEmpty(x.Value));
            Assert.DoesNotContain(result, x => string.IsNullOrEmpty(x.Count));
            Assert.DoesNotContain(result, x => x.IsSelected);
        }

        public static IEnumerable<object[]> TorrentsIndexTestData() =>
            new[]
            {
                new object[] { 1, 5, null, 5 },
                new object[] { 2, 7, new FiltrationViewModel(string.Empty, Enumerable.Empty<string>(), long.MinValue, long.MaxValue), 5 },
                new object[] { 1, 10, new FiltrationViewModel("Torrent", new []{ "1", "3", "5", "10" }, 100000, long.MaxValue), 7 } 
            };

        public void Dispose() => _context?.Dispose();
    }
}