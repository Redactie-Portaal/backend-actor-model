using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Grains.GrainState
{
    public class NewsItemGrainState
    {
        public string Title { get; set; }

        public Status Status { get; set; }

        public ApprovalState ApprovalState { get; set; }

        public string Author { get; set; }

        public FeedSource Source { get; set; }

        public string Body { get; set; }

        public List<Guid> ContactDetails { get; set; }
        
        public Location LocationDetails { get; set; }

        public DateTime ProductionDate { get; set; }

        public DateTime EndDate { get; set; }

        public Category Category { get; set; }

        public Region Region { get; set; }

        public string Dossier { get; set; }

        public List<Guid> Videos { get; set; }

        public List<Guid> Audio { get; set; }

        public List<Guid> Photos { get; set; }
    }
}
