using FluentValidation;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.NewsItem;

public class NewsItemValidator : AbstractValidator<NewsItemModel>
{
    public NewsItemValidator()
    {
        this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        this.RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
        this.RuleFor(x => x.Source).NotEmpty().WithMessage("Source is required");
        this.RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required");
        this.RuleFor(x => x.ContactDetails).NotEmpty().WithMessage("ContactDetails is required");
        this.RuleFor(x => x.LocationDetails).NotEmpty().WithMessage("LocationDetails is required");
    }
}
