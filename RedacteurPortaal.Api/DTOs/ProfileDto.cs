using RedacteurPortaal.DomainModels.Profile;
using ContactDetails = RedacteurPortaal.Api.Models.Profile.ContactDetails;

namespace RedacteurPortaal.Api.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public string ProfilePicture { get; set; }

        public Role Role { get; set; }

        public DateTime LastOnline { get; set; }
    }
}