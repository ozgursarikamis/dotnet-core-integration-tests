using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api;
using TestBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TestBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class StockControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public StockControllerTests(WebApplicationFactory<Startup> factory)
        {
            var baseAddress = new Uri("http://localhost/api/stock/");
            Client = factory.CreateDefaultClient(baseAddress);
        }
        
        [Fact]
        public async Task GetStockTotal_ReturnsSuccessStatusCode()
        {
            var response = await Client.GetAsync("total");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetStockTotal_ReturnsExpectedJsonContentString()
        {
            var response = await Client.GetStringAsync("total");
            Assert.Equal("{\"stockItemTotal\":100}", response);
        }

        [Fact]
        public async Task GetStockTotal_ReturnsExpectedJsonContentType()
        {
            var response = await Client.GetAsync("total");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetStockTotal_ReturnsExpectedJson()
        {
            var model = await Client.GetFromJsonAsync<ExpectedStockTotalOutputModel>("total");

            Assert.NotNull(model);
            Assert.True(model.StockItemTotal > 0);
        }
    }
}
