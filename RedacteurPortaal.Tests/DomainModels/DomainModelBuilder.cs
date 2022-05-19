using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Profile;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests.DomainModels
{
    public static class DomainModelBuilder
    {
        public static Profile CreateProfile()
        {
            return new Profile(Guid.NewGuid(), "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        }
    }
}
