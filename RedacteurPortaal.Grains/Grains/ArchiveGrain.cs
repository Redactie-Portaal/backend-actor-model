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
        [PersistentState("archive", "OrleansStorage")]
        IPersistentState<ArchiveModel> archive)
    {
        this.archive = archive;
    }

    public async Task DeleteArchive(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(guid);
        await grain.DeleteArchive(guid);
        await this.archive.ClearStateAsync();
    }

    public async Task AddVideoItem(ArchiveModel archive, MediaVideoItem videoItem)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(archive.Guid);
        await grain.AddVideoItem(archive, videoItem);
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
    }

    public async Task AddPhotoItem(ArchiveModel archive, MediaPhotoItem photoItem)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(archive.Guid);
        await grain.AddPhotoItem(archive, photoItem);
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
    }

    public async Task AddAudioItem(ArchiveModel archive, MediaAudioItem audioItem)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(archive.Guid);
        await grain.AddAudioItem(archive, audioItem);
        this.archive.State = archive;
        await this.archive.WriteStateAsync();
    }

    public async Task<ArchiveModel> GetArchive(Guid guid)
    {
        var grain = this.GrainFactory.GetGrain<IArchiveGrain>(guid);
        return await Task.FromResult(this.archive.State);
    }
}