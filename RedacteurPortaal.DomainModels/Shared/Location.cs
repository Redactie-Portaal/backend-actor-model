using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class Location
{
    public Location(string name, string city, string province, string street, string zip, string latitude, string longitude)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.City = city ?? throw new ArgumentNullException(nameof(city));
        this.Province = province ?? throw new ArgumentNullException(nameof(province));
        this.Street = street ?? throw new ArgumentNullException(nameof(street));
        this.Zip = zip ?? throw new ArgumentNullException(nameof(zip));
        this.Latitude = latitude ?? throw new ArgumentNullException(nameof(latitude));
        this.Longitude = longitude ?? throw new ArgumentNullException(nameof(longitude));
    }

    public Guid Id { get; }

    public string Name { get; }

    public string City { get; }

    public string Province { get; }

    public string Street { get; }

    public string Zip { get; }

    public string Latitude { get; }

    public string Longitude { get; }
}