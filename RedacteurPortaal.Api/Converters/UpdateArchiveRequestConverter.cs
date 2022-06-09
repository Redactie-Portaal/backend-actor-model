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
            var photoItems = new List<MediaPhotoItem>();
            var videoItems = new List<MediaVideoItem>();
            var audioItems = new List<MediaAudioItem>();
            var newsItems = new List<NewsItemModel>();
            if (item.MediaAudioItems != null)
            {
                foreach (var i in item.MediaAudioItems)
                {
                    var y = new MediaAudioItem(i.Id,
                                               i.Title,
                                               i.Folder,
                                               i.RepublishDate,
                                               i.Rights,
                                               i.Camera,
                                               i.LastWords,
                                               i.ProxyFile,
                                               i.Presentation,
                                               new Location(i.Location.Id, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
                                               i.Format,
                                               i.MediaLocation,
                                               TimeSpan.FromSeconds(i.DurationSeconds),
                                               i.Weather,
                                               i.FirstWords,
                                               i.ProgramName);

                    audioItems.Add(y);
                }
            }

            if (item.MediaPhotoItems != null)
            {
                foreach (var i in item.MediaPhotoItems)
                {
                    var y = new MediaPhotoItem(i.Id,
                                               i.Title,
                                               i.Folder,
                                               i.RepublishDate,
                                               i.Rights,
                                               i.Camera,
                                               i.LastWords,
                                               i.ProxyFile,
                                               i.Presentation,
                                               new Location(i.Location.Id, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
                                               i.Format,
                                               i.MediaLocation,
                                               i.Image);

                    photoItems.Add(y);
                }
            }

            if (item.MediaVideoItems != null)
            {
                foreach (var i in item.MediaVideoItems)
                {
                    var y = new MediaVideoItem(i.Id,
                                               i.Title,
                                               i.Folder,
                                               i.RepublishDate,
                                               i.Rights,
                                               i.Camera,
                                               i.LastWords,
                                               i.ProxyFile,
                                               i.Presentation,
                                               new Location(i.Location.Id, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
                                               i.Format,
                                               i.Reporter,
                                               i.Sound,
                                               i.Editor,
                                               i.LastPicture,
                                               i.Keywords,
                                               i.VoiceOver,
                                               i.Description,
                                               i.ProgramDate,
                                               i.ItemName,
                                               i.EPG,
                                               TimeSpan.FromSeconds(i.DurationSeconds),
                                               i.ArchiveMaterial,
                                               i.Weather,
                                               i.Producer,
                                               i.Director,
                                               i.Guests,
                                               i.FirstPicture,
                                               i.ProgramName,
                                               i.FirstWords,
                                               i.MediaLocation);

                    videoItems.Add(y);
                }
            }

            if (item.NewsItems != null)
            {
                foreach (var items in item.NewsItems)
                {
                    var vidItems = new List<MediaVideoItem>();
                    foreach (var vid in items.Videos)
                    {
                        var z = new MediaVideoItem(Guid.Empty,
                                                   vid.Title,
                                                   vid.Folder,
                                                   vid.RepublishDate,
                                                   vid.Rights,
                                                   vid.Camera,
                                                   vid.LastWords,
                                                   vid.ProxyFile,
                                                   vid.Presentation,
                                                   new Location(Guid.Empty, vid.Location.Name, vid.Location.City, vid.Location.Province, vid.Location.Street, vid.Location.Zip, vid.Location.Latitude, vid.Location.Longitude),
                                                   vid.Format,
                                                   vid.Reporter,
                                                   vid.Sound,
                                                   vid.Editor,
                                                   vid.LastPicture,
                                                   vid.Keywords,
                                                   vid.VoiceOver,
                                                   vid.Description,
                                                   vid.ProgramDate,
                                                   vid.ItemName,
                                                   vid.EPG,
                                                   TimeSpan.FromSeconds(vid.DurationSeconds),
                                                   vid.ArchiveMaterial,
                                                   vid.Weather,
                                                   vid.Producer,
                                                   vid.Director,
                                                   vid.Guests,
                                                   vid.FirstPicture,
                                                   vid.ProgramName,
                                                   vid.FirstWords,
                                                   vid.MediaLocation);
                        
                        videoItems.Add(z);
                    }

                    var feedSource = items.Source?.Adapt<FeedSource>();
                    var contacts = items.ContactDetails.Adapt<List<Contact>>();
                    var location = items.LocationDetails.Adapt<Location>();
                    var audItems = new List<MediaAudioItem>();
                    foreach (var aud in items.Audio)
                    {
                        var z = new MediaAudioItem(Guid.Empty,
                                                   aud.Title,
                                                   aud.Folder,
                                                   aud.RepublishDate,
                                                   aud.Rights,
                                                   aud.Camera,
                                                   aud.LastWords,
                                                   aud.ProxyFile,
                                                   aud.Presentation,
                                                   new Location(Guid.Empty, aud.Location.Name, aud.Location.City, aud.Location.Province, aud.Location.Street, aud.Location.Zip, aud.Location.Latitude, aud.Location.Longitude),
                                                   aud.Format,
                                                   aud.MediaLocation,
                                                   TimeSpan.FromSeconds(aud.DurationSeconds),
                                                   aud.Weather,
                                                   aud.FirstWords,
                                                   aud.ProgramName);

                        audioItems.Add(z);
                    }

                    var photItem = new List<MediaPhotoItem>();

                    foreach (var photo in items.Photos)
                    {
                        var z = new MediaPhotoItem(Guid.Empty,
                                                   photo.Title,
                                                   photo.Folder,
                                                   photo.RepublishDate,
                                                   photo.Rights,
                                                   photo.Camera,
                                                   photo.LastWords,
                                                   photo.ProxyFile,
                                                   photo.Presentation,
                                                   new Location(Guid.Empty, photo.Location.Name, photo.Location.City, photo.Location.Province, photo.Location.Street, photo.Location.Zip, photo.Location.Latitude, photo.Location.Longitude),
                                                   photo.Format,
                                                   photo.MediaLocation,
                                                   photo.Image);

                        photoItems.Add(z);
                    }

#pragma warning disable CS8604 // Possible null reference argument.
                    ApprovalState approvalState = (ApprovalState)Enum.Parse(typeof(ApprovalState), items.ApprovalState);

                    newsItems.Add(new NewsItemModel(id,
                                     items.Title,
                                     items.Status,
                                     approvalState,
                                     items.Author,
                                     feedSource,
                                     items.Body,
                                     contacts,
                                     location,
                                     items.ProductionDate,
                                     items.EndDate,
                                     items.Category,
                                     items.Region,
                                     videoItems,
                                     audioItems,
                                     photoItems));
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
