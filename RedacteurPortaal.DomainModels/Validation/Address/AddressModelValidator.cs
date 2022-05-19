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
    public class AddressModelValidator : AbstractValidator<AddressModel>
    {
        public AddressModelValidator()
        {
            this.RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required");
            this.RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            this.RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required");
            this.RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                .Matches(new Regex(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)")).WithMessage("Phone number is not valid");
            this.RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address is required").EmailAddress().WithMessage("Email address is invalid");
            this.RuleFor(x => x.Webpage).NotEmpty().WithMessage("Webpage is required");

            this.RuleFor(x => x.PostalCode).Matches(new Regex(
               @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$"));
        }
    }
}
