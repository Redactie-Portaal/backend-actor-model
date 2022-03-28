namespace RedacteurPortaal.DomainModels.NewsItem
{
    [Serializable]
    public class ItemBody
    {
        public Guid Guid { get;set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}
