using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test
{
    [CollectionDefinition("Col")]
    public class ClusterCollection : ICollectionFixture<ClusterFixture>
    {

    }
}
