using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface ISourceGrain : IManageableGrain<Source>
{
    Task AddSource(Source source);
    
    Task Update(Source source);
}