using System.ComponentModel.DataAnnotations;

namespace RedacteurPortaal.Data.Models
{
    public class GrainReference
    {
        [Key]
        public Guid GrainId { get; set; }
        public string TypeName { get; set; }
    }
}
