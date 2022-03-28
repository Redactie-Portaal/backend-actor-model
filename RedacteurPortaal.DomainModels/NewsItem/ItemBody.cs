namespace RedacteurPortaal.DomainModels.NewsItem
{
    [Serializable]
    public class ItemBody
    {
        public Guid Guid { get;set; }
        public string Des { get; set; }
        public string Short { get; set; }
    }
}
