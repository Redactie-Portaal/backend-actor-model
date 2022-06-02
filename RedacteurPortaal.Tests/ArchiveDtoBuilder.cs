using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests;

public static class ArchiveDtoBuilder
{
    public static MediaVideoItemDto CreateMediaVideoItemRequest()
    {
        return new MediaVideoItemDto() {
            Title = "Video 1",
            Folder = "Folder 1",
            RepublishDate = DateTime.Now,
            Rights = "Rights 1",
            Camera = "Camera 1",
            LastWords = "Last Words 1",
            ProxyFile = "ProxyFile 1",
            Presentation = "Presentation 1",
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 0, Longitude = 0, Street = "Street", Zip = "1000AB" },
            Format = "Format 1",
            MediaLocation = new Uri("http://www.google.com"),
            Reporter = "Reporter 1",
            Sound = "Sound 1",
            Editor = "Editor 1",
            LastPicture = "Last Picture 1",
            Keywords = new() { "Keyword 1", "Keyword 2" },
            VoiceOver = "Voice Over 1",
            Description = "Description 1",
            ProgramDate = DateTime.Now,
            ItemName = "Item Name 1",
            EPG = "EPG 1",
            DurationSeconds = 1,
            ArchiveMaterial = "ArchiveMaterial",
            Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
            Producer = "Producer 1",
            Director = "Director 1",
            Guests = new() { "Guest 1", "Guest 2" },
            FirstPicture = "First Picture 1",
            ProgramName = "Program Name 1",
            FirstWords = "First Words 1",
        };
    }

    public static MediaAudioItemDto CreateMediaAudioItemRequest()
    {
        return new MediaAudioItemDto() {
            DurationSeconds = TimeSpan.FromSeconds(1),
            FirstWords = "firstwords",
            ProgramName = "programname",
            Title = "Video 1",
            Folder = "Folder 1",
            RepublishDate = DateTime.Now,
            Rights = "Rights 1",
            Camera = "Camera 1",
            LastWords = "Last Words 1",
            ProxyFile = "ProxyFile 1",
            Presentation = "Presentation 1",
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
            Format = "Format 1",
            MediaLocation = new Uri("http://www.google.com"),
            Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
        };
    }

    public static MediaPhotoItemDto CreateMediaPhotoItemRequest()
    {
        return new MediaPhotoItemDto() {
            Title = "Photo 1",
            Folder = "Folder 1",
            RepublishDate = DateTime.Now,
            Rights = "Rights 1",
            Camera = "Camera 1",
            LastWords = "Last Words 1",
            ProxyFile = "ProxyFile 1",
            Presentation = "Presentation 1",
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
            Format = "Format 1",
            MediaLocation = new Uri("http://www.google.com"),
            Image = "Image 1",
        };
    }

    public static UpdateArchiveRequest BuildUpdateArchiveRequest()
    {
        return new UpdateArchiveRequest() {
            Title = "New Title",
            Label = "New Label",
            MediaPhotoItems = new() {
                new MediaPhotoItemDto() {
                    Title = "Photo 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Image = "Image 1",
                }
            },
            MediaVideoItems = new() {
                new MediaVideoItemDto() {
                    Title = "Video 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Reporter = "Reporter 1",
                    Sound = "Sound 1",
                    Editor = "Editor 1",
                    LastPicture = "Last Picture 1",
                    Keywords = new() { "Keyword 1", "Keyword 2" },
                    VoiceOver = "Voice Over 1",
                    Description = "Description 1",
                    ProgramDate = DateTime.Now,
                    ItemName = "Item Name 1",
                    EPG = "EPG 1",
                    DurationSeconds = 1,
                    ArchiveMaterial = "archivematerial",
                    Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
                    Producer = "Producer 1",
                    Director = "Director 1",
                    Guests = new() { "Guest 1", "Guest 2" },
                    FirstPicture = "First Picture 1",
                    ProgramName = "Program Name 1",
                    FirstWords = "First Words 1",
                }
            },
            MediaAudioItems = new() {
                new MediaAudioItemDto() {
                    DurationSeconds = TimeSpan.FromSeconds(1),
                    FirstWords = "firstwords",
                    ProgramName = "programname",
                    Title = "Video 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
                }
            },
            NewsItems = new(),
            Scripts = new() { "script1","script2"},
        };
    }

    public static CreateArchiveRequest BuildAddArchiveRequest()
    {
        return new CreateArchiveRequest() {
            Title = "Title",
            Label = "Label",
            MediaPhotoItems = new() {
                new MediaPhotoItemDto() {
                    Title = "Photo 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Image = "Image 1",
                }
            },
            MediaVideoItems = new() {
                new MediaVideoItemDto() {
                    Title = "Video 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Reporter = "Reporter 1",
                    Sound = "Sound 1",
                    Editor = "Editor 1",
                    LastPicture = "Last Picture 1",
                    Keywords = new() { "Keyword 1", "Keyword 2" },
                    VoiceOver = "Voice Over 1",
                    Description = "Description 1",
                    ProgramDate = DateTime.Now,
                    ItemName = "Item Name 1",
                    EPG = "EPG 1",
                    DurationSeconds = 1,
                    ArchiveMaterial = "archiveMaterials",
                    Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
                    Producer = "Producer 1",
                    Director = "Director 1",
                    Guests = new() { "Guest 1", "Guest 2" },
                    FirstPicture = "First Picture 1",
                    ProgramName = "Program Name 1",
                    FirstWords = "First Words 1",
                }
            },
            MediaAudioItems = new() {
                new MediaAudioItemDto() {
                    DurationSeconds = TimeSpan.FromSeconds(1),
                    FirstWords = "firstwords",
                    ProgramName = "programname",
                    Title = "Video 1",
                    Folder = "Folder 1",
                    RepublishDate = DateTime.Now,
                    Rights = "Rights 1",
                    Camera = "Camera 1",
                    LastWords = "Last Words 1",
                    ProxyFile = "ProxyFile 1",
                    Presentation = "Presentation 1",
                    Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "5050AB" },
                    Format = "Format 1",
                    MediaLocation = new Uri("http://www.google.com"),
                    Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
                }
            },
            NewsItems = new(),
            ArchivedDate = DateTime.Now,
            Scripts = new() { "script 1", "script 2" },
        };
    }

    public static UpdateArchiveRequest BuildSmallestArchive()
    {
        return new UpdateArchiveRequest() {

            Title = "Title",
            Label = "Label",
            Scripts = new List<string> { "Scripts"}
        };
    }
}
