using Orleans;

namespace RedacteurPortaal.Grains
{
    public static class OrleansClient
    {
        public static IClusterClient ClusterClient { get; set; } 
    }
}
