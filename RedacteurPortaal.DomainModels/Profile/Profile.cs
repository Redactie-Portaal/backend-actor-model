using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.DomainModels.Validation.Profile;

namespace RedacteurPortaal.DomainModels.Profile
{
    public class Profile : IBaseEntity
    {
        public Profile()
        {
        }

        public Profile(Guid id, string fullName, ContactDetails contactDetails, string profilePicture, Role role, DateTime lastOnline)
        {
            this.Id = id;
            this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
            this.ProfilePicture = profilePicture ?? throw new ArgumentNullException(nameof(profilePicture));
            this.Role = role;
            this.LastOnline = lastOnline;
        }

        public Guid Id { get; init; }

        public string FullName { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public string ProfilePicture { get; set; }

        public Role Role { get; private set; }

        public DateTime LastOnline { get; private set; }
    }
}
