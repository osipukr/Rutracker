using System.Net.Http;
using Xunit;

namespace Rutracker.IntegrationTests.WebApi.Controllers.Base
{
    public class BaseTestController : IClassFixture<WebApiFactory>
    {
        protected readonly HttpClient _client;

        protected BaseTestController(WebApiFactory factory) => _client = factory.CreateClient();
    }
}