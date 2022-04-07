using Orleans;
using RedacteurPortaal.DomainModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IProfileGrain : IGrainWithGuidKey, IManageableGrain<Profile>
    {
        public Task<Profile> Update(ProfileUpdate profile);
    }
}
