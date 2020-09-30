using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Merchandise.Api.Diagnostics;
using TennisBookings.Merchandise.Api.External.Database;
using TestBookings.Merchandise.Api.IntegrationTests.Fakes;

namespace TestBookings.Merchandise.Api.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        public FakeCloudDatabase FakeCloudDatabase { get; set; }

        public CustomWebApplicationFactory()
        {
            FakeCloudDatabase = FakeCloudDatabase.WithDefaultProducts();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<ICloudDatabase>(FakeCloudDatabase);
                services.AddSingleton<IMetricRecorder>(new FakeMetricRecorder());
            });
        }
    }
}
