﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api;
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
    }
}