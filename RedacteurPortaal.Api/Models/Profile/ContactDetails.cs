using Newtonsoft.Json;

namespace RedacteurPortaal.Api.Models.Profile
{
    public class ContactDetails
    {
        public string PhoneNumber { get; set; }
       
        public string Email { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Province { get; set; }
    }
}