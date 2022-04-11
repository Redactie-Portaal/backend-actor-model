using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Validation.NewsItem
{
    internal class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            // Phone number regex
            this.RuleFor(x => x.TelephoneNumber).Matches(new Regex(
                "^(?:(?:\\(?(?:00|\\+)([1-4]\\d\\d|[1-9]\\d+)\\)?)[\\-\\.\\ \\\\\\/]?)?((?:\\(?\\d{1,}\\)?[\\-\\.\\ \\\\\\/]?){0,})(?:[\\-\\.\\ \\\\\\/]?(?:#|ext\\.?|extension|x)[\\-\\.\\ \\\\\\/]?(\\d+))?$"));

        }
    }
}
