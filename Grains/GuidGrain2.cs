using Microsoft.Extensions.Logging;
using Orleans;

namespace GrainInterfaces
{
    public class GuidGrain2 : Grain, IGuidGrain2
    {
        private readonly ILogger logger;

        private IClusterClient _client;

        public GuidGrain2(ILogger<GuidGrain2> logger, IClusterClient client)
        {
            this.logger = logger;
            _client = client;
        }

        public async Task<string> DoGrain2(string greeting)
        {
            await Task.Delay(5000);
            return await Task.FromResult(greeting);
        }

    }
}
