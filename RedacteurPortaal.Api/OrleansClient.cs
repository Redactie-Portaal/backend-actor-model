using Orleans;

namespace RedacteurPortaal.Api
{
    public static class OrleansClient
    {
        public static IClusterClient ClusterClient { get; set; }
    }
}
