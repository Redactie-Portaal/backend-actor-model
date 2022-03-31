using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class NewsItemDTO
{
    public NewsItemDTO(
        string title,
        string status,
        string author,
        string contactDetails,
        string locationDetails,
        string produtionDate,
        string category,
        string region)
    {
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Status = status;
        this.Author = author ?? throw new ArgumentNullException(nameof(author));
        this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
        this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
        this.ProdutionDate = produtionDate;
        this.Category = category;
        this.Region = region;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; }

    public string Status { get; }

    public string Author { get; }

    public string ContactDetails { get; }

    public string LocationDetails { get; }

    public string ProdutionDate { get; }

    public string Category { get; }

    public string Region { get; }
}
