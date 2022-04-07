﻿using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.DomainModels.Archive;

    class ArchiveModel
{
    public ArchiveModel(
        string label,
        List<MediaPhotoItem> mediaPhotoItems,
        List<MediaVideoItem> mediaVideoItems,
        List<MediaAudioItem> mediaAudioItems,
        DateTime archivedDate,
        List<string> scripts)
    {
        this.Label = label ?? throw new ArgumentNullException(nameof(label));
        this.MediaPhotoItems = mediaPhotoItems;
        this.MediaVideoItems = mediaVideoItems;
        this.MediaAudioItems = mediaAudioItems;
        this.ArchivedDate = archivedDate;
        this.Scripts = scripts;
    }

    public string Label { get; }

    public List<MediaPhotoItem> MediaPhotoItems { get; }

    public List<MediaVideoItem> MediaVideoItems { get; }

    public List<MediaAudioItem> MediaAudioItems { get; }

    public DateTime ArchivedDate { get; }

    public List<string> Scripts { get; }
}

