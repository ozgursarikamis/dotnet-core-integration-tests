using Xunit;

namespace TennisBookings.Web.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CustomWebApplicationFactory<Startup> Factory { get; }

        public AuthenticationTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            Factory = factory;
        }
    }
}
