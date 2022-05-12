using FluentValidation;
using RedacteurPortaal.DomainModels.Adress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Address
{
    public class AddressValidator : AbstractValidator<AddressModel>
    {
        public AddressValidator()
            {
            this.RuleFor(x => x.CompanyName).NotEmpty();
            this.RuleFor(x => x.Address).NotEmpty();

            // Postal code regex
            this.RuleFor(x => x.PostalCode).Matches(new Regex(
               @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$"));

            // Email address regex
            this.RuleFor(x => x.EmailAddress).Matches(new Regex(
                @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"));
        }
    }
}



/*POSTALCODE EXAMPLES
 * 
 * @"^[0234567]\d{5}$*/
