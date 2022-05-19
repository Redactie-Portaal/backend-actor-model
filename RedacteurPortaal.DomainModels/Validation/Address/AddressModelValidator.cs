using FluentValidation;
using RedacteurPortaal.DomainModels.Adress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Address
{
    public class AddressModelValidator : AbstractValidator<AddressModel>
    {
        // this.CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
        //this.Address = address ?? throw new ArgumentNullException(nameof(address));
        //this.PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        //this.PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        //this.EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
        //this.Webpage = webpage ?? throw new ArgumentNullException(nameof(webpage));
        public AddressModelValidator()
        {
            this.RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required");
            this.RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            this.RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required");
            this.RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            this.RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address is required");
            this.RuleFor(x => x.Webpage).NotEmpty().WithMessage("Webpage is required");
        }
    }
}
