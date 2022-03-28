using RedacteurPortaal.DomainModels.NewsItem.Media;

namespace RedacteurPortaal.DomainModels.NewsItem
{
    [Serializable]
    public class NewsItemModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public StatusEnum Status { get; set; }
        public string Author { get; set; }
        public FeedSource Source { get; set; }
        public ItemBody Body { get; set; } 
        public string ContactDetails { get; set; }
        public string LocationDetails { get; set; }
        public DateTime ProdutionDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryEnum Category { get; set; }
        public RegionEnum Region { get; set; }
        public MediaVideoItem Video { get; set; }
        public MediaAudioItem Audio { get; set; }
        public MediaPhotoItem Photo { get; set; }
        //public ? SourceDetails { get; set; }
    }
}
