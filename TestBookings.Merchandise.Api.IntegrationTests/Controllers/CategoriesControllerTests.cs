using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api;
using Xunit;

namespace TestBookings.Merchandise.Api.IntegrationTests.Controllers
{
    // test a controller:

    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient HttpClient { get; }

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            HttpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            var response = await HttpClient.GetAsync("/api/categories");
            response.EnsureSuccessStatusCode();
        }
    }
}
