using Orleans;
using RedacteurPortaal.DomainModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IProfileGrain : IManageableGrain<Profile>
{     
    Task<Profile> Update(Profile profile);
}
