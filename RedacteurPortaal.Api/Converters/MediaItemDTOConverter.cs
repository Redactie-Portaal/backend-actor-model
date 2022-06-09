using Mapster;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System.Collections.Generic;

namespace RedacteurPortaal.Api.Converters
{
    public static class MediaItemDTOConverter
    {
        public static MediaVideoItem ConvertMediaVideoDTO(this MediaVideoItemDto item, Guid id)
        {
            var x = new MediaVideoItem(id,
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
            return x;
        }

        public static MediaAudioItem ConvertMediaAudioDTO(this MediaAudioItemDto item, Guid id)
        {
            var x = new MediaAudioItem(id,
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

            return x;
        }

        public static MediaPhotoItem ConvertMediaPhotoDTO(this MediaPhotoItemDto item, Guid id)
        {
            var x = new MediaPhotoItem(id,
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

            return x;
        }
    }
}