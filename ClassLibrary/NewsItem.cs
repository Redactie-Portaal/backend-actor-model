namespace ClassLibrary
{
    [Serializable]
    public class NewsItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Idea { get; set; }
        public string ContactDetails { get; set; }
        //public ? province { get; set; }
        public DateTime ProdutionDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryEnum Category { get; set; }
        public RegionEnum Region{ get; set; }
        public string Location { get; set; }
        //public ? SourceDetails { get; set; }

    }
}