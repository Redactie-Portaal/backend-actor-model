using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.DomainModels.Validation.Shared;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {    
        this.RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        this.RuleFor(x => x.City).NotEmpty().WithMessage("Please enter a city");
        this.RuleFor(x => x.Province).NotEmpty().WithMessage("Please enter a province");
        this.RuleFor(x => x.Street).NotEmpty().WithMessage("Please enter a street");
        this.RuleFor(x => x.Latitude).LessThanOrEqualTo(90).GreaterThanOrEqualTo(-90);
        this.RuleFor(x => x.Longitude).LessThanOrEqualTo(180).GreaterThanOrEqualTo(-180);

        // Postal code regex ^[1-9][0-9]{3}[[:space:]]{0,1}(?!SA|SD|SS)[A-Z]{2}$
        this.RuleFor(x => x.Zip).Matches(new Regex(@"^[1-9][0-9]{3}[\S]{0,1}(?!SA|SD|SS)[A-Z]{2}$"));
    }
}