using Mapster;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System.Collections.Generic;

namespace RedacteurPortaal.Api.Converters
{
    public static class UpdateNewsItemRequestConverter
    {
        public static NewsItemModel AsDomainModel(this UpdateNewsItemRequest request, Guid id)
        {
            var videoItems = new List<MediaVideoItem>();
            if (request.Videos is not null)
            {
                foreach (var item in request.Videos)
                {
                    var x = new MediaVideoItem(Guid.Empty,
                                               item.Title,
                                               item.Folder,
                                               item.RepublishDate,
                                               item.Rights,
                                               item.Camera,
                                               item.LastWords,
                                               item.ProxyFile,
                                               item.Presentation,
                                               new Location(Guid.Empty, item.Location.Name, item.Location.City, item.Location.Province, item.Location.Street, item.Location.Zip, item.Location.Latitude, item.Location.Longitude),
                                               item.Format,
                                               item.Reporter,
                                               item.Sound,
                                               item.Editor,
                                               item.LastPicture,
                                               item.Keywords,
                                               item.VoiceOver,
                                               item.Description,
                                               item.ProgramDate,
                                               item.ItemName,
                                               item.EPG,
                                               TimeSpan.FromSeconds(item.DurationSeconds),
                                               item.ArchiveMaterial,
                                               item.Weather,
                                               item.Producer,
                                               item.Director,
                                               item.Guests,
                                               item.FirstPicture,
                                               item.ProgramName,
                                               item.FirstWords,
                                               item.MediaLocation);

                    videoItems.Add(x);
                }
            }

            var feedSource = request.Source.Adapt<FeedSource>();
            var contacts = new List<Contact>();
            foreach (var item in request.ContactDetails)
            {
                var x = new Contact(Guid.Empty, item.Name, item.Email, item.TelephoneNumber);
                contacts.Add(x);
            }
            //var contacts = request.ContactDetails.Adapt<List<Contact>>();
            var location = request.LocationDetails.Adapt<Location>();
            var audioItems = new List<MediaAudioItem>();
            if (request.Audio is not null)
            {
                foreach (var item in request.Audio)
                {
                    var x = new MediaAudioItem(Guid.Empty,
                                               item.Title,
                                               item.Folder,
                                               item.RepublishDate,
                                               item.Rights,
                                               item.Camera,
                                               item.LastWords,
                                               item.ProxyFile,
                                               item.Presentation,
                                               new Location(Guid.Empty, item.Location.Name, item.Location.City, item.Location.Province, item.Location.Street, item.Location.Zip, item.Location.Latitude, item.Location.Longitude),
                                               item.Format,
                                               item.MediaLocation,
                                               TimeSpan.FromSeconds(item.DurationSeconds),
                                               item.Weather,
                                               item.FirstWords,
                                               item.ProgramName);

                    audioItems.Add(x);
                }
            }

            var photoItems = new List<MediaPhotoItem>();

            if (request.Photos is not null)
            {
                foreach (var item in request.Photos)
                {
                    var x = new MediaPhotoItem(Guid.Empty,
                                               item.Title,
                                               item.Folder,
                                               item.RepublishDate,
                                               item.Rights,
                                                       item.Camera,
                                                       item.LastWords,
                                                       item.ProxyFile,
                                                       item.Presentation,
                                                       new Location(Guid.Empty, item.Location.Name, item.Location.City, item.Location.Province, item.Location.Street, item.Location.Zip, item.Location.Latitude, item.Location.Longitude),
                                                       item.Format,
                                                       item.MediaLocation,
                                                       item.Image);

                    photoItems.Add(x);
                }
            }

            var approvalState = Enum.TryParse(request.ApprovalState, out ApprovalState state);

#pragma warning disable CS8604 // Possible null reference argument.
            return new NewsItemModel(id,
                                     request.Title,
                                     request.Status,
                                     state,
                                     request.Author,
                                     feedSource,
                                     request.Body,
                                     contacts,
                                     location,
                                     request.ProductionDate,
                                     request.EndDate,
                                     request.Category,
                                     request.Region,
                                     videoItems,
                                     audioItems,
                                     photoItems);
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
