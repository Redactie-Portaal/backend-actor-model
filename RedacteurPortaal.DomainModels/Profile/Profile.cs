using FluentValidation;
using RedacteurPortaal.DomainModels.Validation.Profile;

namespace RedacteurPortaal.DomainModels.Profile;

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
        
        new ProfileValidator().ValidateAndThrow(this);
    }

    public Guid Id { get; set; }

    public string FullName { get; private set; }

    public ContactDetails ContactDetails { get; private set; }

    public string ProfilePicture { get; private set; }

    public Role Role { get; private set; }

    public DateTime LastOnline { get; private set; }
}
