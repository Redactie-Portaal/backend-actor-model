using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models;

[Serializable]
public class NewsItemDetailDTO
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public Status? Status { get; set; }

    public string? Author { get; set; }

    public FeedSource? Source { get; set; }

    public ItemBody? Body { get; set; }

    public List<Contact> ContactDetails { get; set; }

    public Location LocationDetails { get; set;  }

    public DateTime? ProdutionDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Category? Category { get; set; }

    public Region? Region { get; set; }

    public MediaVideoItemDTO[] Videos { get; set; }

    public MediaAudioItemDTO[] Audio { get; set; }

    public MediaPhotoItemDTO[] Photos { get; set; }
}