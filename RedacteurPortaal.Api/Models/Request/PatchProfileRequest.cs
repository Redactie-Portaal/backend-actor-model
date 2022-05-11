using RedacteurPortaal.Api.Models.Profile;

namespace RedacteurPortaal.Api.Models.Request
{
    public class PatchProfileRequest
    {
        public string Name { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public string ProfilePicture { get; set; }
    }
}
