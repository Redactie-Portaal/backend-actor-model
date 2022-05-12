using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Api.Models.Request;

public class UpdateNewsItemRequest
{
    public string Title { get; set; }

    public Status Status { get; set; }

    public ApprovalState? ApprovalState { get; set; }

    public string? Author { get; set; }

    public FeedSourceDto Source { get; set; }

    public string Body { get; set; }

    public List<ContactDto> ContactDetails { get; set; }

    public LocationDto LocationDetails { get; set; }

    public DateTime ProductionDate { get; set; }

    public DateTime EndDate { get; set; }

    public Category Category { get; set; }

    public Region Region { get; set; }

    public MediaVideoItemDto[] Videos { get; set; }

    public MediaAudioItemDto[] Audio { get; set; }

    public MediaPhotoItemDto[] Photos { get; set; }
}
