namespace RedacteurPortaal.ClassLibrary.NewsItem
{
    [Serializable]
    public class NewsItemModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        //public Description Description { get; set; }
        public string Idea { get; set; }
        public string ContactDetails { get; set; }
        public string LocationDetails { get; set; }
        public DateTime ProdutionDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryEnum Category { get; set; }
        public RegionEnum Region { get; set; }
        public StatusEnum Status { get; set; }  
        //public ? SourceDetails { get; set; }

    }
}
