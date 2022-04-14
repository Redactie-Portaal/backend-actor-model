using System.Runtime.CompilerServices;
﻿using Mapster;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.DomainModels.NewsItem;

[Serializable]
public class NewsItemModel : IBaseEntity
{
    public NewsItemModel()
    {
    }

    public NewsItemModel(
        Guid id,
        string title,
        Status status,
        string author,
        FeedSource source,
        ItemBody body,
        List<Contact> contactDetails,
        Location locationDetails,
        DateTime produtionDate,
        DateTime endDate,
        Category category,
        Region region,
        MediaVideoItem[] videos,
        MediaAudioItem[] audio,
        MediaPhotoItem[] photos)
    {
        this.Id = id;
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Status = status;
        this.Author = author ?? throw new ArgumentNullException(nameof(author));
        this.Source = source ?? throw new ArgumentNullException(nameof(source));
        this.Body = body ?? throw new ArgumentNullException(nameof(body));
        this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
        this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
        this.ProdutionDate = produtionDate;
        this.EndDate = endDate;
        this.Category = category;
        this.Region = region;
        this.Videos = videos ?? throw new ArgumentNullException(nameof(videos));
        this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
        this.Photos = photos ?? throw new ArgumentNullException(nameof(photos));
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public Status Status { get; set; }

    public string Author { get; set; }

    public FeedSource Source { get; set; }

    public ItemBody Body { get; set; }

    public List<Contact> ContactDetails { get; set; }

    public Location LocationDetails { get; }

    public DateTime ProdutionDate { get; set; }

    public DateTime EndDate { get; set; }

    public Category Category { get; set; }

    public Region Region { get; set; }

    public MediaVideoItem[] Videos { get; set; }

    public MediaAudioItem[] Audio { get; set; }

    public MediaPhotoItem[] Photos { get; set; }
}