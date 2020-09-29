using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api;
using TestBookings.Merchandise.Api.IntegrationTests.Models;
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

        [Fact]
        public async Task GetAll_ReturnsExpectedMediaType()
        {
            var response = await HttpClient.GetAsync("/api/categories");
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetAll_ReturnsContent()
        {
            var response = await HttpClient.GetAsync("api/categories");
            Assert.NotNull(response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedJson()
        {
            var responseStream = await HttpClient.GetStreamAsync("api/categories");
            var model = await JsonSerializer
                .DeserializeAsync<ExpectedCategoriesModel>(responseStream,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            
            Assert.NotNull(model?.AllowedCategories);
        }
    }
}
