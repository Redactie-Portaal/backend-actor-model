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
        Name = name;
        City = city;
        Province = province;
        Street = street;
        Zip = zip;
        Latitude = latitude ?? throw new ArgumentNullException(nameof(latitude));
        Longitude = longitude ?? throw new ArgumentNullException(nameof(longitude));
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