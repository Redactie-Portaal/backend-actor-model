using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains;

public class ArchiveGrain : Grain, IArchiveGrain
{
    private readonly IPersistentState<ArchiveModel> archive;

    public ArchiveGrain(
        [PersistentState("archiveModel", "OrleansStorage")]
        IPersistentState<ArchiveModel> archive)
    {
        this.archive = archive;
    }

    public Task RemoveArchive()
    {
        throw new NotImplementedException();
    }

    public Task AddVideoItem(MediaVideoItem videoItem)
    {
        throw new NotImplementedException();
    }

    public Task AddPhotoItem(MediaPhotoItem photoItem)
    {
        throw new NotImplementedException();
    }

    public Task AddAudioItem(MediaAudioItem audioItem)
    {
        throw new NotImplementedException();
    }

    public async Task<ArchiveModel> GetArchive(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(guid);
        return await Task.FromResult(this.archive.State);
    }
}