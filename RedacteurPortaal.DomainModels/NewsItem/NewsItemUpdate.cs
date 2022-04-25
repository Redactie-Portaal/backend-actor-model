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
        public NewsItemUpdate(Guid id, string title, Status status, ApprovalState approvalState, string author, FeedSource source, ItemBody body,
            List<Contact> contactDetails, Location locationDetails, DateTime productionDate, DateTime endDate,
            Category category, Region region, MediaVideoItem[] videos, MediaAudioItem[] audio, MediaPhotoItem[] photos)
        {
            this.Id = id;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Status = status;
            this.Author = author ?? throw new ArgumentNullException(nameof(author));
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
            this.Body = body ?? throw new ArgumentNullException(nameof(body));
            this.ContactDetails = contactDetails ?? throw new ArgumentNullException(nameof(contactDetails));
            this.LocationDetails = locationDetails ?? throw new ArgumentNullException(nameof(locationDetails));
            this.ProductionDate = productionDate;
            this.EndDate = endDate;
            this.Category = category;
            this.Region = region;
            this.Videos = videos ?? throw new ArgumentNullException(nameof(videos));
            this.Audio = audio ?? throw new ArgumentNullException(nameof(audio));
            this.Photos = photos ?? throw new ArgumentNullException(nameof(photos));
        }

        public Guid Id { get; private set; }

        public string Title { get; private set; }
            
        public Status Status { get; private set; }

        public ApprovalState ApprovalState { get; private set; }
      
        public string Author { get; private set; }

        public FeedSource Source { get; private set; }

        public ItemBody Body { get; private set; }

        public List<Contact> ContactDetails { get; private set; }

        public Location LocationDetails { get; private set; }

        public DateTime ProductionDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public Category Category { get; private set; }

        public Region Region { get; private set; }

        public MediaVideoItem[] Videos { get; private set; }

        public MediaAudioItem[] Audio { get; private set; }

        public MediaPhotoItem[] Photos { get; private set; }
    }
}
