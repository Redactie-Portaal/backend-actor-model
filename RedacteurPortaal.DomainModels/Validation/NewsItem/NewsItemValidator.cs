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
        //this.RuleFor(x => x.ContactDetails).Cascade(CascadeMode.Stop).NotNull().WithMessage("ContactDetails is required").Must(col => col == null || col.Count() != 0).WithMessage("ContactDetails is required");
        //this.RuleForEach(x => x.ContactDetails).NotEmpty().NotNull().WithMessage("Contact Details is required");
        //this.RuleFor(x => x.ContactDetails).Must(list => object.ReferenceEquals(null, list)).NotEmpty();
        //this.RuleFor(x => x.ContactDetails).NotNull().DependentRules(() => {
        //    this.RuleFor(x => x.ContactDetails.Count == 0).NotEmpty().WithMessage("Contact details is required");
        //});
        this.RuleFor(x => x.LocationDetails).NotNull().DependentRules(() =>
        {
            this.RuleFor(x => x.LocationDetails.Latitude).NotNull().WithMessage("Latitude is required");
            this.RuleFor(x => x.LocationDetails.Longitude).NotNull().WithMessage("Longitude is required");
            this.RuleFor(x => x.LocationDetails.Name).NotEmpty().WithMessage("LocationName is required");
            this.RuleFor(x => x.LocationDetails.Street).NotEmpty().WithMessage("Street is required");
            this.RuleFor(x => x.LocationDetails.City).NotEmpty().WithMessage("City is required");
            this.RuleFor(x => x.LocationDetails.Province).NotEmpty().WithMessage("Country is required");
            this.RuleFor(x => x.LocationDetails.Zip).NotEmpty().WithMessage("PostalCode is required");
        });
    }
}
