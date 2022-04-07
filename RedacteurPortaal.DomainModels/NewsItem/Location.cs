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
        this.Name = name;
        this.City = city;
        this.Province = province;
        this.Street = street;
        this.Zip = zip;
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