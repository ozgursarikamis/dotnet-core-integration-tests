using System;
using System.Net;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api;
using Xunit;

namespace TestBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class AdminHomeControllerTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> Factory { get; }

        public AdminHomeControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            Factory = factory;
        }

        [Fact]
        public async Task Get_SecurePageIsForbiddenForAnUnauthenticatedUser()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("/Admin");
             
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/identity/account/login", 
                response.Headers.Location.OriginalString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
