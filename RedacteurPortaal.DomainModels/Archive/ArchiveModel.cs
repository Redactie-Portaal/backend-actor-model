using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Validation.Archive;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Archive;

public class ArchiveModel : IBaseEntity
{
    public ArchiveModel()
    {
    }
    
    public ArchiveModel(
        Guid id,
        string title,
        string label,
        List<Guid> mediaPhotoItems,
        List<Guid> mediaVideoItems,
        List<Guid> mediaAudioItems,
        List<Guid> newsItems,
        DateTime archivedDate,
        List<string> scripts)
    {
        this.Id = id;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Label = label ?? throw new ArgumentNullException(nameof(label));
        this.MediaPhotoItems = mediaPhotoItems ?? throw new ArgumentNullException(nameof(mediaPhotoItems));
        this.MediaVideoItems = mediaVideoItems ?? throw new ArgumentNullException(nameof(mediaVideoItems));
        this.MediaAudioItems = mediaAudioItems ?? throw new ArgumentNullException(nameof(mediaAudioItems));
        this.NewsItems = newsItems ?? throw new ArgumentNullException(nameof(newsItems));
        this.ArchivedDate = archivedDate;
        this.Scripts = scripts ?? throw new ArgumentNullException(nameof(scripts));

        new ArchiveModelValidator().ValidateAndThrow(this);
    }

    public Guid Id { get; set; }

    public string Title { get; private set; }

    public string Label { get; private set; }

    public List<Guid> MediaPhotoItems { get; private set; }

    public List<Guid> MediaVideoItems { get; private set; }

    public List<Guid> MediaAudioItems { get; private set; }
    
    public List<Guid> NewsItems { get; private set; }

    public DateTime ArchivedDate { get; private set; }

    public List<string> Scripts { get; private set;  }
}