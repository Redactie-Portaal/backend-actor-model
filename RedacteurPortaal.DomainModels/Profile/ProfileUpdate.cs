using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Profile
{
    public class ProfileUpdate
    {
        public ProfileUpdate()
        {
        }

        public ProfileUpdate(string name, string profilePicture, ContactDetails contactDetails)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ProfilePicture = profilePicture ?? throw new ArgumentNullException(nameof(profilePicture));
            this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
        }

        public string Name { get; private set; }

        public string ProfilePicture { get; private set; }

        public ContactDetails ContactDetails { get; private set; }
    }
}
