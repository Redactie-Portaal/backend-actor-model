﻿namespace RedacteurPortaal.ClassLibrary.NewsItem
{
    public class NewsItemRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public ItemBody Description { get; set; }
        public string Idea { get; set; }
        public string ContactDetails { get; set; }
        //public ? LocationDetails { get; set; }
        public DateTime ProdutionDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryEnum Category { get; set; }
        public RegionEnum Region { get; set; }
        public string Location { get; set; }
        //public ? SourceDetails { get; set; }
    }
}
