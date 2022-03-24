
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        private readonly ILogger _logger;


        public TestGrain(ILogger<TestGrain> logger)
        {
            _logger = logger;
        }

        public async Task<string> Test()
        {
            return "hallo";
        }
    }
}
