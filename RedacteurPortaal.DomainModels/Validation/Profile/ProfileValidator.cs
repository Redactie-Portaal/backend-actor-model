using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Validation.Profile;

public class ProfileValidator : AbstractValidator<RedacteurPortaal.DomainModels.Profile.Profile>
{
    public ProfileValidator()
    {
        this.RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
        this.RuleFor(x => x.ProfilePicture).NotEmpty().WithMessage("Profile picture is required");
        this.RuleFor(x => x.ContactDetails).NotNull().DependentRules(() =>
        {
            this.RuleFor(x => x.ContactDetails.Email).NotEmpty().WithMessage("Email is required");
            this.RuleFor(x => x.ContactDetails.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            this.RuleFor(x => x.ContactDetails.Address).NotEmpty().WithMessage("Address is required");
            this.RuleFor(x => x.ContactDetails.City).NotEmpty().WithMessage("City is required");
            this.RuleFor(x => x.ContactDetails.Province).NotEmpty().WithMessage("Province is required");
            this.RuleFor(x => x.ContactDetails.PostalCode).NotEmpty().WithMessage("Country is required");
        });
    }
}
