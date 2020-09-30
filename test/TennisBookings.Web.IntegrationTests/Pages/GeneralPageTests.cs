using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public class GeneralPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CustomWebApplicationFactory<Startup> Factory { get; }

        public GeneralPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
        }

        public static IEnumerable<object[]> ValidUrls => new List<object[]>
        {
            new object[] {"/"}, new object[] {"/Index"}, new object[] {"/About"}, new object[] {"/Enquiry"}
        };

        [Theory, MemberData(nameof(ValidUrls))]
        public async Task ValidUrls_ReturnSuccessAndExpectedContentType(string path)
        {
            var expected = new MediaTypeHeaderValue("text/html");

            var client = Factory.CreateClient();

            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            Assert.Equal(expected.MediaType, response.Content.Headers.ContentType.MediaType);
        }
    }
}
