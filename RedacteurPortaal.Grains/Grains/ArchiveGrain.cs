using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains

    public class ArchiveGrain : Grain, IArchiveGrain
{
    private readonly IPersistentState<ArchiveModel> archiveModel;

    public ArchiveGrain(
        [PersistentState("archiveModel", "OrleansStorage")]
        IPersistentState<archiveModel> archiveModel)
    {
        this.archiveModel = archiveModel;
    }

    public async Task RemoveArchive()
    {
        throw new NotImplementedException();
    }

    public async Task AddVideoItem(MediaVideoItem item)
    {
        throw new NotImplementedException();
    }

    public async Task AddPhotoItem(MediaPhotoItem item)
    {
        throw new NotImplementedException();
    }

    public async Task AddAudioItem(MediaAudioItem item)
    {
        throw new NotImplementedException();
    }

    public async Task<ArchiveModel> GetArchive(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(guid);

        return null;
    }
}

