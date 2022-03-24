using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task<string> Test();
    }
}
