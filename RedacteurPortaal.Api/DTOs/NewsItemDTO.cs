using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.Models;

[Serializable]
public class NewsItemDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Status { get; set; }

    public string ApprovalStatus { get; set; }
  
    public string? Author { get; set; }

    public FeedSourceDto? Source { get; set; }

    public string? Body { get; set; }

    public List<ContactDto> ContactDetails { get; set; }

    public LocationDto LocationDetails { get; set;  }

    public DateTime? ProdutionDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Category? Category { get; set; }

    public Region? Region { get; set; }

    public MediaVideoItemDto[] Videos { get; set; }

    public MediaAudioItemDto[] Audio { get; set; }

    public MediaPhotoItemDto[] Photos { get; set; }
}