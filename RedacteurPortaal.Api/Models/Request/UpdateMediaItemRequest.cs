namespace RedacteurPortaal.Api.Models.Request
{
    public abstract class UpdateMediaItemRequest
    {
        public string Title { get; set; }

        public string Folder { get; set; }

        public DateTime RepublishDate { get; set; }

        public string Rights { get; set; }

        public string Camera { get; set; }

        public string LastWords { get; set; }

        public string ProxyFile { get; set; }

        public string Presentation { get; set; }

        public UpdateLocationRequest Location { get; set; }

        public string Format { get; set; }

        public Uri MediaLocation { get; set; }
    }
}
