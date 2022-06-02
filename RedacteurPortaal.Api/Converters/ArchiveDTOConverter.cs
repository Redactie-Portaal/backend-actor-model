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
    public static class ArchiveDTOConverter
    {
        public static ArchiveModel ConvertArchiveDTO(this ArchiveDto item, Guid id)
        {
            var photoItems = new List<MediaPhotoItem>();
            var videoItems = new List<MediaVideoItem>();
            var audioItems = new List<MediaAudioItem>();
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
                                               new Location(Guid.Empty, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
                                               i.Format,
                                               i.MediaLocation,
                                               i.DurationSeconds,
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
                                               new Location(Guid.Empty, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
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
                                               new Location(Guid.Empty, i.Location.Name, i.Location.City, i.Location.Province, i.Location.Street, i.Location.Zip, i.Location.Latitude, i.Location.Longitude),
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

            var x = new ArchiveModel(
            id,
            item.Title,
            item.Label,
            photoItems,
            videoItems,
            audioItems,
            new(),
            item.ArchivedDate = DateTime.Now,
            item.Scripts);
            return x;
        }
    }
}
