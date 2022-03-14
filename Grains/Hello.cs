using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class Hello : Grain, IHello
    {
        private readonly ILogger logger;

        private IClusterClient _client;

        public Hello(ILogger<Hello> logger, IClusterClient client)
        {
            this.logger = logger;
            _client = client;
        }

        public async Task<string> SayHello(string greeting)
        {
            var grain = _client.GetGrain<IBye>(0);
            var response = await grain.SayBye("Bye");
            logger.LogInformation(response);
            var dosomethingrain = _client.GetGrain<ITestingGrain>(0);
            var res = await dosomethingrain.DoSomething("hallo");
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return await Task.FromResult($"\n Client said: '{greeting}', so ByeGrain says: {response}! Recieved {res}");
        }

    }
}
