using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.Extensions.Configuration;
using TennisBookings.Web.IntegrationTests.Helpers;
using Xunit;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public class HomePageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> Factory { get; }

        public HomePageTests(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
        }

        [Fact]
        public async Task Get_ReturnsPageWithExpected()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var h1 = content.QuerySelector("h1");

            Assert.Equal("Welcome to Tennis by the Sea!", h1.TextContent);
        }

        public static IEnumerable<object[]> ConfigVariations => new List<object[]>
        {
            // global, page, should show:
            new object[] { false, false, false },
            new object[] { true, false, false },
            new object[] { false, true, false },
            new object[] { true, true, true },
        };

        [Theory]
        [MemberData(nameof(ConfigVariations))]
        public async Task HomePageIncludesForecast_ForExpactations(
            bool globalEnabled, bool pageEnabled, bool shouldShow)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        {"Features:WeatherForecasting:EnableWeatherForecast", globalEnabled.ToString()},
                        {"Features:HomePage:EnableWeatherForecast", pageEnabled.ToString()},
                    });
                });
            }).CreateClient();

            var response = await client.GetAsync("/");

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var forecastDiv = content.All
                .SingleOrDefault(e => e.Id == "weather-forecast" && e.LocalName == TagNames.Div);

            Assert.Equal(shouldShow, forecastDiv != null);
        }
    }
}
