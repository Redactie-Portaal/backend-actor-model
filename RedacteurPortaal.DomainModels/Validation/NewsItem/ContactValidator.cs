using System.Text.RegularExpressions;
using FluentValidation;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.DomainModels.Validation.NewsItem;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        this.RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        this.RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
        this.RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        this.RuleFor(x => x.TelephoneNumber).Matches(new Regex(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)"));

        //// Phone number regex
        //this.RuleFor(x => x.TelephoneNumber).Matches(new Regex(
        //    "^(?:(?:\\(?(?:00|\\+)([1-4]\\d\\d|[1-9]\\d+)\\)?)[\\-\\.\\ \\\\\\/]?)?((?:\\(?\\d{1,}\\)?[\\-\\.\\ \\\\\\/]?){0,})(?:[\\-\\.\\ \\\\\\/]?(?:#|ext\\.?|extension|x)[\\-\\.\\ \\\\\\/]?(\\d+))?$"));
    }
}
