using System.Threading.Tasks;
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
    }
}
