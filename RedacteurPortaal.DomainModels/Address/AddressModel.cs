namespace RedacteurPortaal.DomainModels.Adress
{
    // [Serializable]
    public class AddressModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Webpage { get; set; }
       

    }
}
