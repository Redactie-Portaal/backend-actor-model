using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.Profile;

namespace RedacteurPortaal.DomainModels.Validation.Profile
{
    public class ContactDetailsValidator : AbstractValidator<ContactDetails>
    {
        public ContactDetailsValidator()
        {
            // Postal code regex ^[1-9][0-9]{3}[[:space:]]{0,1}(?!SA|SD|SS)[A-Z]{2}$
            this.RuleFor(x => x.PostalCode).Matches(new Regex(@"^[1-9][0-9]{3}[\s]{0,1}(?!SA|SD|SS)[A-Z]{2}$"));

            // Phone number regex
            this.RuleFor(x => x.PhoneNumber).Matches(new Regex(
                "^(?:(?:\\(?(?:00|\\+)([1-4]\\d\\d|[1-9]\\d+)\\)?)[\\-\\.\\ \\\\\\/]?)?((?:\\(?\\d{1,}\\)?[\\-\\.\\ \\\\\\/]?){0,})(?:[\\-\\.\\ \\\\\\/]?(?:#|ext\\.?|extension|x)[\\-\\.\\ \\\\\\/]?(\\d+))?$"));
        }
    }
}
