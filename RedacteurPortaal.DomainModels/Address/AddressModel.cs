namespace RedacteurPortaal.DomainModels.Adress
{
    // [Serializable]
    public class AddressModel : IBaseEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Webpage { get; set; }

        public AddressModel(Guid id, string companyName, string address, string postalCode, string phoneNumber, string emailAddress, string webpage)
        {
            this.Id = id;
            this.CompanyName= companyName;
            this.Address = address;
            this.PostalCode = postalCode;
            this.PhoneNumber = phoneNumber;
            this.EmailAddress = emailAddress;
            this.Webpage = webpage;
        }

        public AddressModel()
        {
        }
    }
}
