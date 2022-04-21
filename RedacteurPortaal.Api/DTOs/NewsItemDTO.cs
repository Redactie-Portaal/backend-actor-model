using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models;

public class NewsItemDto
{
    public Guid? Id { get; set; } = Guid.NewGuid();

    public string? Title { get; set; }

    public string? Status { get; set; }

    public string? Author { get; set; }

    public string? ContactDetails { get; set; }

    public string? LocationDetails { get; set; }

    public string? ProdutionDate { get; set; }

    public string? Category { get; set; }

    public string? Region { get; set; }
}
