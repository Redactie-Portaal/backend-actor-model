using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.NewsItem;

public class Location
{
    public Location()
    {    
    }
    
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

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Street { get; set; }

    public string Zip { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}