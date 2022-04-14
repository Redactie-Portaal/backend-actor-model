using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.DomainModels.NewsItem
{
    public class NewsItemUpdate
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public Status Status { get; private set; }

        public string Author { get; private set; }

        public FeedSource Source { get; private set; }

        public ItemBody Body { get; private set; }

        public List<Contact> ContactDetails { get; private set; }

        public Location LocationDetails { get; private set; }

        public DateTime ProdutionDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public Category Category { get; private set; }

        public Region Region { get; private set; }

        public MediaVideoItem[] Videos { get; private set; }

        public MediaAudioItem[] Audio { get; private set; }

        public MediaPhotoItem[] Photos { get; private set; }
    }
}
