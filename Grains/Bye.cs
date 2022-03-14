using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class Bye : Grain, IBye
    {
        private readonly ILogger logger;

        public Bye(ILogger<Bye> logger)
        {
            this.logger = logger;
        }

        public async Task<string> SayBye(string greeting)
        {
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return await Task.FromResult("Bye Grain says bye.");
        }
    }
}
