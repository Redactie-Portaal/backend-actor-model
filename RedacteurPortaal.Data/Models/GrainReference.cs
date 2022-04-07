using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Data.Models
{
    public class GrainReference
    {
        [Key]
        public Guid GrainId { get; set; }
        public string TypeName { get; set; }
    }
}
