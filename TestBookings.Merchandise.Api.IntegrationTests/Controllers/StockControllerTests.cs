using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Merchandise.Api;
using TennisBookings.Merchandise.Api.Data.Dto;
using TennisBookings.Merchandise.Api.External.Database;
using TestBookings.Merchandise.Api.IntegrationTests.Fakes;
using TestBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TestBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class StockControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }
        public WebApplicationFactory<Startup> Factory;

        public StockControllerTests(WebApplicationFactory<Startup> factory)
        {
            var baseAddress = new Uri("http://localhost/api/stock/");
            factory.ClientOptions.BaseAddress = baseAddress;

            Client = factory.CreateClient();
            Factory = factory;
        }
        
        [Fact]
        public async Task GetStockTotal_ReturnsSuccessStatusCode()
        {
            var response = await Client.GetAsync("total");
            response.EnsureSuccessStatusCode();
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

        [Fact]
        public async Task GetStockTotal_ReturnsExpectedStockQuantity()
        {
            var cloudDatabase = new FakeCloudDatabase(new []
            {
                new ProductDto{ StockCount = 200}, 
                new ProductDto{ StockCount = 300}, 
                new ProductDto{ StockCount = 500}, 
            });

            // Registering a service:
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICloudDatabase>(cloudDatabase);
                });
            }).CreateClient();

            var model = await client.GetFromJsonAsync<ExpectedStockTotalOutputModel>("total");
            
            Assert.Equal(1000, model.StockItemTotal);
        }
    }
}
