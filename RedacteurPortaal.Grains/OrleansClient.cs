using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains
{
    public static class OrleansClient
    {
        public static IClusterClient ClusterClient { get; set; }
    }
}
