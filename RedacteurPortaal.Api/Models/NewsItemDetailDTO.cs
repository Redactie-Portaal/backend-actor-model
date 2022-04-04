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
    public NewsItemDetailDTO()
    {

    }
    //public NewsItemDetailDTO(
    //    string title,
    //    string status,
    //    string author,
    //    FeedSource source,
    //    string body,
    //    string contactDetails,
    //    string locationDetails,
    //    DateTime produtionDate,
    //    DateTime endDate,
    //    string category,
    //    string region
    //    //MediaVideoItem video,
    //    //MediaAudioItem audio,
    //    //MediaPhotoItem photo
    //    )
    //{
    //    this.Title = title ?? throw new ArgumentNullException(nameof(title));
    //    this.Status = status;
    //    this.Author = author ?? throw new ArgumentNullException(nameof(author));
    //    this.Source = source ?? throw new ArgumentNullException(nameof(source));
    //    this.BodyDescription = body ?? throw new ArgumentNullException(nameof(body));
    //    this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
    //    this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
    //    this.ProdutionDate = produtionDate;
    //    this.EndDate = endDate;
    //    this.Category = category;
    //    this.Region = region;
    //    //this.Video = video ?? throw new ArgumentNullException(nameof(video));
    //    //this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
    //    //this.Photo = photo ?? throw new ArgumentNullException(nameof(photo));
    //}

    public string Title { get; set; }

    public string Status { get; set; }

    public string Author { get; set; }

    public FeedSource Source { get; set; }

    public string BodyDescription { get; set; }

    public string ContactDetails { get; set; }

    public string LocationDetails { get; set; }

    public DateTime ProdutionDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Category { get; set; }

    public string Region { get; set; }

    //public MediaVideoItem Video { get; set; }

    //public MediaAudioItem Audio { get; set; }

    //public MediaPhotoItem Photo { get; set; }
}

//public Guid Id { get; set; }
//public string Title { get; set; }
//public Status Status { get; set; }
//public string Author { get; set; }
//public FeedSource Source { get; set; }
//public ItemBody Body { get; set; }
//public string ContactDetails { get; set; }
//public string LocationDetails { get; set; }
//public DateTime ProdutionDate { get; set; }
//public DateTime EndDate { get; set; }
//public Category Category { get; set; }
//public Region Region { get; set; }
//public MediaVideoItem Video { get; set; }
//public MediaAudioItem Audio { get; set; }
//public MediaPhotoItem Photo { get; set; }