using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class TestingGrain : Grain, ITestingGrain
    {
        private readonly ILogger logger;

        private IClusterClient _client;

        public TestingGrain(ILogger<Hello> logger, IClusterClient client)
        {
            this.logger = logger;
            _client = client;
        }

        public async Task<string> DoSomething(string greeting)
        {
            var endTime = DateTime.Now.AddSeconds(10);

            //while (true)
            //{
            //    if (DateTime.Now >= endTime)
            //        break;
            //}
            return await Task.FromResult($"Yoooooo");
        }
    }
}
