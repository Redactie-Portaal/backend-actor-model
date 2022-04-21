using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class Source
{
    public Source() { }
    public Source(Guid id, string name, string uri, DateTime date)
    {
        this.Id = id;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        this.Date = date;
    }

    public Guid Id { get; }

    public string Name { get; }
    
    public string Uri { get; }

    public DateTime Date { get; }
}
