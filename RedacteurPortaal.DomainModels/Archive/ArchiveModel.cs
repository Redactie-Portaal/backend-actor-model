using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.DomainModels.Archive;

public class ArchiveModel : IBaseEntity
{
    public ArchiveModel(
        Guid id,
        string title,
        string label,
        List<MediaPhotoItem> mediaPhotoItems,
        List<MediaVideoItem> mediaVideoItems,
        List<MediaAudioItem> mediaAudioItems,
        DateTime archivedDate,
        List<string> scripts)
    {
        this.Id = id;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Label = label ?? throw new ArgumentNullException(nameof(label));
        this.MediaPhotoItems = mediaPhotoItems ?? throw new ArgumentNullException(nameof(mediaPhotoItems));
        this.MediaVideoItems = mediaVideoItems ?? throw new ArgumentNullException(nameof(mediaVideoItems));
        this.MediaAudioItems = mediaAudioItems ?? throw new ArgumentNullException(nameof(mediaAudioItems));
        this.ArchivedDate = archivedDate;
        this.Scripts = scripts ?? throw new ArgumentNullException(nameof(scripts));
    }

    public ArchiveModel()
    {
        
    }
    public Guid Id { get; set; }

    public string Title { get; private set; }

    public string Label { get; private set; }

    public List<MediaPhotoItem> MediaPhotoItems { get; private set; }

    public List<MediaVideoItem> MediaVideoItems { get; private set; }

    public List<MediaAudioItem> MediaAudioItems { get; private set; }

    public DateTime ArchivedDate { get; private set; }

    public List<string> Scripts { get; private set;  }
}