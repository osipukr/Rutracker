using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Rutracker.Infrastructure.Data;
using Rutracker.Infrastructure.Data.Extensions;
using Rutracker.Server;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;
using Xunit;

namespace Rutracker.IntegrationTests.Server.Controllers
{
    public class TorrentsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TorrentsControllerTests(WebApplicationFactory<Startup> factory) =>
            _client = factory.WithWebHostBuilder(config =>
                config.ConfigureServices(services =>
                {
                    services
                        .AddEntityFrameworkInMemoryDatabase()
                        .AddDbContext<TorrentContext>((provider, options) =>
                        {
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                            options.UseInternalServiceProvider(provider);
                        });

                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<TorrentContext>();

                    context.Database.EnsureCreated();
                    context.SeedAsync().Wait();
                }))
                .CreateClient();

        [Fact(DisplayName = "GetTorrentsIndexAsync(page,pageSize,filter) should return torrents index page")]
        public async Task Controller_GetTorrentsIndexAsync_Should_Return_Torrents_Page()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int expectedCount = page * pageSize;

            // Act
            var model = await _client.PostJsonAsync<TorrentsIndexViewModel>(
                $"/api/torrents/paging/?page={page}&pageSize={pageSize}",
                null);

            // Assert
            Assert.NotNull(model);
            Assert.NotNull(model.TorrentItems);
            Assert.NotNull(model.PaginationModel);

            Assert.Equal(page, model.PaginationModel.CurrentPage);
            Assert.Equal(pageSize, model.PaginationModel.PageSize);
            Assert.Equal(expectedCount, model.TorrentItems.Length);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync(id) should return torrent details page")]
        public async Task Controller_GetTorrentIndexAsync_Should_Return_Torrent_Details_Page()
        {
            // Arrange
            const long expectedId = 5;

            // Act
            var model = await _client.GetJsonAsync<TorrentIndexViewModel>($"/api/torrents/?id={expectedId}");

            // Assert
            Assert.NotNull(model);
            Assert.NotNull(model.TorrentDetailsItem);
            Assert.Equal(expectedId, model.TorrentDetailsItem.Id);
        }

        [Fact(DisplayName = "GetTitlesAsync(count) should return forum titles list")]
        public async Task Controller_GetTitlesAsync_Should_Return_Forum_Title_Facets()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var model = await _client.GetJsonAsync<FacetItemViewModel[]>($"/api/torrents/titles/?count={expectedCount}");

            // Assert
            Assert.NotNull(model);
            Assert.Equal(expectedCount, model.Length);
        }

        [Fact(DisplayName = "GetTorrentsIndexAsync() with negative number should return BadRequest")]
        public async Task Controller_GetTorrentsIndexAsync_NegativeNumber_Should_Return_BadRequest()
        {
            // Arrange
            const long page = -10;
            const long pageSize = 10;

            // Act
            var data = JsonConvert.SerializeObject(null);
            using var content = new ByteArrayContent(Encoding.UTF8.GetBytes(data));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            var response = await _client.PostAsync($"/api/torrents/paging/?page={page}&pageSize={pageSize}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "GetTorrentIndexAsync() with negative number should return BadRequest")]
        public async Task Controller_GetTorrentIndexAsync_NegativeNumber_Should_Return_BadRequest()
        {
            // Arrange
            const long id = -10;

            // Act
            var response = await _client.GetAsync($"/api/torrents/?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "GetTitlesAsync() with negative number should return BadRequest")]
        public async Task Controller_GetTitlesAsync_NegativeNumber_Should_Return_BadRequest()
        {
            // Arrange
            const int count = -10;

            // Act
            var response = await _client.GetAsync($"/api/torrents/titles/?count={count}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}