namespace RedacteurPortaal.Api.DTOs
{
    public class AddressDTO
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }  

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Webpage { get; set; }
    }
}
