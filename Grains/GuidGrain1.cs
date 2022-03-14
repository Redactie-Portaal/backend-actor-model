using Microsoft.Extensions.Logging;
using Orleans;

namespace GrainInterfaces
{
    public class GuidGrain1 : Grain, IGuidGrain1
    {
        private readonly ILogger logger;

        private IClusterClient _client;


        public GuidGrain1(ILogger<GuidGrain1> logger, IClusterClient client)
        {
            this.logger = logger;
            _client = client;
        }

        public async Task<string> DoGrain(string greeting, Guid guid)
        {
            var grain = _client.GetGrain<IGuidGrain2>(guid);
            var response = await grain.DoGrain2(greeting);
            return await Task.FromResult(response); 

        }
    }
}
