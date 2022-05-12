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
        //this.RuleFor(x => x.FullName);
        this.RuleFor(x => x.FullName).Must(this.CheckIfString);
        //this.RuleFor(x => x.FullName).NotEqual(string.Empty);
        //this.RuleFor(x => x.FullName).NotEqual();
        //this.RuleFor(x => x.FullName).
        this.RuleFor(x => x.ProfilePicture).NotEmpty().WithMessage("Profile picture is required");

    }
    
        private bool CheckIfString(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str)) { return false; }
            return true;
        }
}
