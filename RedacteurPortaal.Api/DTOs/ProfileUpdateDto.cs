using RedacteurPortaal.Api.Models.Profile;

namespace RedacteurPortaal.Api.DTOs
{
    public class ProfileUpdateDto
    {
        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        public ContactDetails ContactDetails { get; set; }
    }
}