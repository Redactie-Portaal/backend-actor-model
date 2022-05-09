using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.Validation.Shared;

namespace RedacteurPortaal.DomainModels.Shared;

public class Location
{
    public Location()
    {    
    }
    
    public Location(Guid id, string name, string city, string province, string street, string zip, decimal latitude, decimal longitude)
    {
        this.Id = id;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.City = city ?? throw new ArgumentNullException(nameof(city));
        this.Province = province ?? throw new ArgumentNullException(nameof(province));
        this.Street = street ?? throw new ArgumentNullException(nameof(street));
        this.Zip = zip ?? throw new ArgumentNullException(nameof(zip));
        this.Longitude = longitude;
        this.Latitude = latitude;

        new LocationValidator().ValidateAndThrow(this);
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Street { get; set; }

    public string Zip { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}