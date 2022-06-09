using Mapster;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System.Collections.Generic;

namespace RedacteurPortaal.Api.Converters
{
    public static class UpdateArchiveRequestConverter
    {
        public static ArchiveModel AsDomainModel(this UpdateArchiveRequest item, Guid id)
        {
            var photoItems = new List<Guid>();
            var videoItems = new List<Guid>();
            var audioItems = new List<Guid>();
            var newsItems = new List<Guid>();

            if (item.MediaPhotoItems != null)
            {
                foreach (var i in item.MediaPhotoItems)
                {
                    photoItems.Add(i);
                }
            }

            if (item.MediaVideoItems != null)
            {
                foreach (var i in item.MediaVideoItems)
                {
                    videoItems.Add(i);
                }
            }
            
            if (item.MediaAudioItems != null)
            {
                foreach (var i in item.MediaAudioItems)
                {
                    audioItems.Add(i);
                }
            }

            if (item.NewsItems != null)
            {
                foreach (var i in item.NewsItems)
                {
                    newsItems.Add(i);
                }
            }

            var x = new ArchiveModel(
            id,
            item.Title,
            item.Label,
            photoItems,
            videoItems,
            audioItems,
            newsItems,
            DateTime.UtcNow,
            item.Scripts);
            return x;
        }
    }
}
