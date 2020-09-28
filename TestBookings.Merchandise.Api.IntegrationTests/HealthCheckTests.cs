using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api;
using Xunit;

namespace TestBookings.Merchandise.Api.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Factory { get; }

        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            Factory = factory.CreateDefaultClient();
        }
    }
}
