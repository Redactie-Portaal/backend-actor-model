using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.Grains.GrainInterfaces;

namespace RedacteurPortaal.Grains.Grains
{
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

        }
        public async Task AddVideoItem(MediaVideoItem item)
        {

        }
        public async Task AddPhotoItem(MediaPhotoItem item)
        {
        }
        public async Task AddAudioItem(MediaAudioItem item)
        {
        }
        public async Task<ArchiveModel> GetArchive(Guid guid)
        {
            var grain = this.GrainFactory.GetGrain<IArchiveGrain>(guid);

            return null;
        }
    }
}
