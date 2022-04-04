using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models;

public class NewsItemDetailDTO
{
    public NewsItemDetailDTO(
        string title,
        string status,
        string author,
        FeedSource source,
        ItemBody body,
        string contactDetails,
        string locationDetails,
        string produtionDate,
        string endDate,
        string category,
        string region,
        MediaVideoItem video,
        MediaAudioItem audio,
        MediaPhotoItem photo)
    {
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
        this.Video = video ?? throw new ArgumentNullException(nameof(video));
        this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
        this.Photo = photo ?? throw new ArgumentNullException(nameof(photo));
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; }

    public string Status { get; }

    public string Author { get; }

    public FeedSource Source { get; }

    public ItemBody Body { get; }

    public string ContactDetails { get; }

    public string LocationDetails { get; }

    public string ProdutionDate { get; }

    public string EndDate { get; }

    public string Category { get; }

    public string Region { get; }

    public MediaVideoItem Video { get; }

    public MediaAudioItem Audio { get; }

    public MediaPhotoItem Photo { get; }
}