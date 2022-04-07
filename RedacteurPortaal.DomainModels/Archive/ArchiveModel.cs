using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.DomainModels.Archive;

public class ArchiveModel
{
    public ArchiveModel(
        Guid guid,
        string title,
        string label,
        List<MediaPhotoItem> mediaPhotoItems,
        List<MediaVideoItem> mediaVideoItems,
        List<MediaAudioItem> mediaAudioItems,
        DateTime archivedDate,
        List<string> scripts)
    {
        this.Guid = guid;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Label = label ?? throw new ArgumentNullException(nameof(label));
        this.MediaPhotoItems = mediaPhotoItems ?? throw new ArgumentNullException(nameof(mediaPhotoItems));
        this.MediaVideoItems = mediaVideoItems ?? throw new ArgumentNullException(nameof(mediaVideoItems));
        this.MediaAudioItems = mediaAudioItems ?? throw new ArgumentNullException(nameof(mediaAudioItems));
        this.ArchivedDate = archivedDate;
        this.Scripts = scripts ?? throw new ArgumentNullException(nameof(scripts));
    }

    public Guid Guid { get; }

    public string Title { get; }

    public string Label { get; }

    public List<MediaPhotoItem> MediaPhotoItems { get; }

    public List<MediaVideoItem> MediaVideoItems { get; }

    public List<MediaAudioItem> MediaAudioItems { get; }

    public DateTime ArchivedDate { get; }

    public List<string> Scripts { get; }
}