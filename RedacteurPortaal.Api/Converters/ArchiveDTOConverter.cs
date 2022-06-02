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
        public static ArchiveModel ConvertArchiveDTO(this ArchiveDto item)
        {
            var x = new ArchiveModel(
            Guid.Empty,
            item.Title,
            item.Label,
            new List<MediaPhotoItem>(),
            new List<MediaVideoItem>(),
            new List<MediaAudioItem>(),
            new(),
            item.ArchivedDate = DateTime.Now,
            item.Scripts = new());
            return x;
        }
    }
}
