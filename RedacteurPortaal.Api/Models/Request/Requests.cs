using RedacteurPortaal.Api.Models.Profile;
using System.ComponentModel.DataAnnotations;

namespace RedacteurPortaal.Api.Models.Request
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Struct args.")]
    public record PatchProfileRequest(
        [Required] string Name,
        [Required] string ProfilePicture,
        [Required] ContactDetails ContactDetails);
}
