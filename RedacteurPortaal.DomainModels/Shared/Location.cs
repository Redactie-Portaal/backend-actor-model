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
    public Location(string name, string city, string province, string street, string zip, decimal latitude, decimal longitude)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.City = city ?? throw new ArgumentNullException(nameof(city));
        this.Province = province ?? throw new ArgumentNullException(nameof(province));
        this.Street = street ?? throw new ArgumentNullException(nameof(street));
        this.Zip = zip ?? throw new ArgumentNullException(nameof(zip));
        this.Longitude = longitude;
        this.Latitude = latitude;

        new LocationValidator().ValidateAndThrow(this);

    }

    public Guid Id { get; }

    public string Name { get; }

    public string City { get; }

    public string Province { get; }

    public string Street { get; }

    public string Zip { get; }

    public decimal Latitude { get; }

    public decimal Longitude { get; }
}