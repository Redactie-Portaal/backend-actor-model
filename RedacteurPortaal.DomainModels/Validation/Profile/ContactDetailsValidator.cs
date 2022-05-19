using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.Profile;

namespace RedacteurPortaal.DomainModels.Validation.Profile;

public class ContactDetailsValidator : AbstractValidator<ContactDetails>
{
    public ContactDetailsValidator()
    {
        this.RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        this.RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
        this.RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        this.RuleFor(x => x.PhoneNumber).Matches(new Regex(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)"));
        this.RuleFor(x => x.Province).NotEmpty().WithMessage("Province is required");
        this.RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
        
        // Postal code regex ^[1-9][0-9]{3}[[:space:]]{0,1}(?!SA|SD|SS)[A-Z]{2}$
        this.RuleFor(x => x.PostalCode).Matches(new Regex(@"^[1-9][0-9]{3}[\S]{0,1}(?!SA|SD|SS)[A-Z]{2}$"));
    }
}
