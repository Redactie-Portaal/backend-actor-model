using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface ISourceGrain : IGrainWithGuidKey
{
    Task<Source> GetSource(Guid guid);

    Task AddSource(Source source);

    Task DeleteSource(Guid guid);

    Task UpdateSource(Source source);
}