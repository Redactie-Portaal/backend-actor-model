using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models;

public class NewsItemDTO
{
    public NewsItemDTO()
    {
    }

    public Guid? Id { get; set; } = Guid.NewGuid();

    public string? Title { get; }

    public string? Status { get; }

    public string? Author { get; }

    public string? ContactDetails { get; }

    public string? LocationDetails { get; }

    public string? ProdutionDate { get; }

    public string? Category { get; }

    public string? Region { get; }
}
