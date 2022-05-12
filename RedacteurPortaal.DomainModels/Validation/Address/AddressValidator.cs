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
               @"^[0234567]\d{5}$"));

            // Email address regex
            this.RuleFor(x => x.EmailAddress).Matches(new Regex(
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([azA-Z]{2,4}|[0-9]{1,3})(\]?)$"));
        }
    }
}
