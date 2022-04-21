using Mapster;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.DomainModels.Adress;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.Utility
{
    public class TypeAdapterConfigManager
    {
        public void ÍnitiateNewsItemAdapter()
        {
            // NewsItemModel to DTO
            TypeAdapterConfig<NewsItemModel, NewsItemDetailDto>
            .NewConfig()
            .Map(dest => dest.ContactDetails,
                src => src.ContactDetails.AsQueryable().ProjectToType<ContactDto>(null).ToList())
            .Map(dest => dest.LocationDetails,
                src => src.LocationDetails.Adapt<LocationDto>())
            .Map(dest => dest.Body,
                src => src.Body != null ? src.Body.Adapt<ItemBodyDto>() : new ItemBodyDto())
            .Map(dest => dest.Source,
                src => src.Source != null ? src.Source.Adapt<FeedSourceDto>() : new FeedSourceDto())
            .Map(dest => dest.Videos,
                src => src.Videos.AsQueryable().ProjectToType<MediaVideoItemDto>(null).ToList())
            .Map(dest => dest.Audio,
                src => src.Audio.AsQueryable().ProjectToType<MediaAudioItemDto>(null).ToList())
            .Map(dest => dest.Photos,
                src => src.Photos.AsQueryable().ProjectToType<MediaPhotoItemDto>(null).ToList())
            .Map(dest => dest.Id,
                src => src.Id);

            // DTO to NewsItemModel
            TypeAdapterConfig<NewsItemDetailDto, NewsItemModel>
            .NewConfig()
            .Map(dest => dest.ContactDetails,
                src => src.ContactDetails.AsQueryable().ProjectToType<Contact>(null).ToList())
            .Map(dest => dest.LocationDetails,
                src => src.LocationDetails.Adapt<Location>())
            .Map(dest => dest.Body,
                src => src.Body != null ? src.Body.Adapt<ItemBody>() : new ItemBody())
            .Map(dest => dest.Source,
                src => src.Source != null ? src.Source.Adapt<FeedSource>() : new FeedSource())
            .Map(dest => dest.Videos,
                src => src.Videos.AsQueryable().ProjectToType<MediaVideoItem>(null).ToList())
            .Map(dest => dest.Audio,
                src => src.Audio.AsQueryable().ProjectToType<MediaAudioItem>(null).ToList())
            .Map(dest => dest.Photos,
                src => src.Photos.AsQueryable().ProjectToType<MediaPhotoItem>(null).ToList());
        }

        public void initiateAddressAdapter()
        {
            TypeAdapterConfig<AddressModel, AddressDTO>
            .NewConfig()
            .Map(dest => dest.Id,
                src => src.Id);
        }
    }
}
