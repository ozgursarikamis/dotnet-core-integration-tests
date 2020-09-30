using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Merchandise.Api;
using TestBookings.Merchandise.Api.IntegrationTests.Helpers;
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

        public static IEnumerable<object[]> RoleAccess => new List<object[]>
        {
            new object[] {"Member", HttpStatusCode.Forbidden}, 
            new object[] {"Admin", HttpStatusCode.OK},
        };

        [Theory, MemberData(nameof(RoleAccess))]
        public async Task Get_SecurePageAccessibleOnlyByAdminUsers(
            string role, HttpStatusCode expected)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(
                            "Test", options => options.Role = role);
                });
            }).CreateClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/Admin");
            Assert.Equal(expected, response.StatusCode);
        }
    }
}
