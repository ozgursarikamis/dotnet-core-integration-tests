﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SimulatedCloudSdk.Queue;
using TennisBookings.Merchandise.Api.External.Queue;

namespace TestBookings.Merchandise.Api.IntegrationTests.Fakes
{
    public class FakeCloudQueue : ICloudQueue
    {
        public IList<SendRequest> Requests = new List<SendRequest>();
        public Task<SendResponse> SendAsync(SendRequest request)
        {
            Requests.Add(request);
            return Task.FromResult(new SendResponse {IsSuccess = true, StatusCode = 200});
        }
    }
}
