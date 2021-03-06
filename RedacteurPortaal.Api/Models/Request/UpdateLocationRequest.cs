namespace RedacteurPortaal.Api.Models.Request
{
    public class UpdateLocationRequest
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
