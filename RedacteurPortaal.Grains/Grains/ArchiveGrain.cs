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
    [PersistentState("archive", "OrleansStorage")]
    IPersistentState<ArchiveModel> archive)
    {
        this.archive = archive;
    }

    public async Task Delete()
    {
        await this.archive.ClearStateAsync();
    }

    public async Task AddVideoItem(MediaVideoItem videoItem)
    {
        this.archive.State.MediaVideoItems.Add(videoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task AddPhotoItem(MediaPhotoItem photoItem)
    {
        this.archive.State.MediaPhotoItems.Add(photoItem);
        await this.archive.WriteStateAsync();
    }

    public async Task AddAudioItem(MediaAudioItem audioItem)
    {
        this.archive.State.MediaAudioItems.Add(audioItem);
        await this.archive.WriteStateAsync();
    }

    public async Task<ArchiveModel> Update(ArchiveModel archive)
    {
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
        return await this.Get();
    }

    public Task<ArchiveModel> Get()
    {
        var state = this.archive.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        return Task.FromResult(state);
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.archive.RecordExists);
    }
}