﻿using System;
using System.Collections.Generic;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;

namespace RedacteurPortaal.Tests;

public static class DtoBuilder
{
    public static CreateArchiveRequest BuildAddArchiveRequest()
    {
        return new CreateArchiveRequest() {
            Title = "Title",
            Label = "Label",
            MediaPhotoItems = new(),
            MediaVideoItems = new(),
            MediaAudioItems = new(),
            NewsItems = new(),
            ArchivedDate = DateTime.Now,
            Scripts = new(),
        };
    }

    public static UpdateArchiveRequest BuildUpdateArchiveRequest()
    {
        return new UpdateArchiveRequest() {
            Title = "New Title1",
            Label = "New Label1",
            MediaPhotoItems = new(),
            MediaVideoItems = new(),
            MediaAudioItems = new(),
            NewsItems = new(),
            Scripts = new(),
        };
    }
    public static NewsItemDto BuildAddNewsItemRequest()
    {
        return new NewsItemDto() {
            Author = "Author",
            Audio = new(),
            Body = "foo",
            Category = Category.NEWS,
            Region = Region.LOCAL,
            Photos = new(),
            Videos = new(),
            Source = new FeedSourceDto() {
                PlaceHolder = "foo",
            },
            Status = "DONE",
            Title = "title",
            ApprovalStatus = "APPROVED",
            ContactDetails = new List<ContactDto>(),
            EndDate = DateTime.Now,
            LocationDetails = new LocationDto() {
                City = "foo",
                Latitude = 50,
                Longitude = 50,
                Name = "foo",
                Province = "foo",
                Street = "foo",
                Zip = "5087BB",
            },
            ProdutionDate = DateTime.Now,
        };
    }
    public static UpdateNewsItemRequest BuildUpdateNewsItemRequest()
    {
        return new UpdateNewsItemRequest() {
            Author = "Author1",
            Audio = new(),
            Body = "foo1",
            Category = Category.NEWS,
            Region = Region.LOCAL,
            Photos = new(),
            Videos = new(),
            Source = new FeedSourceDto() {
                PlaceHolder = "foo1",
            },
            Status = Status.DONE,
            Title = "title1",
            ContactDetails = new List<ContactDto>(),
            EndDate = DateTime.Now,
            LocationDetails = new LocationDto() {
                City = "foo1",
                Latitude = 51,
                Longitude = 51,
                Name = "foo1",
                Province = "foo1",
                Street = "foo1",
                Zip = "5087BB1",
            },
        };
    }
    public static AddProfileRequest BuildAddProfileRequest()
    {
        return new AddProfileRequest() {
            Role = Role.ADMIN,
            FullName = "John Doe",
            ContactDetails = new RedacteurPortaal.Api.Models.Profile.ContactDetails() {
                Address = "foo",
                City = "bar",
                Email = "foo@bar.nl",
                PhoneNumber = "0640778812",
                Province = "foo",
                PostalCode = "5087BB"
            },
            LastOnline = DateTime.Now,
            ProfilePicture = "base64"
        };
    }

    public static AddAddressRequest BuildAddAddressRequest()
    {
        return new AddAddressRequest() {
            Address = "FooStreet",
            Webpage = "www.foo.nl",
            CompanyName = "FooBar",
            EmailAddress = "foo@gmail.com",
            PhoneNumber = "0640778812",
            PostalCode = "5087BB"
        };
    }

    public static UpdateAddressRequest BuildPatchAddressRequest()
    {
        return new UpdateAddressRequest() {
            Address = "FooStreet1",
            Webpage = "www.foo1.nl",
            CompanyName = "FooBa1r",
            EmailAddress = "foo1@gmail.com",
            PhoneNumber = "0640778811",
            PostalCode = "9999BB"
        };
    }
    public static PatchProfileRequest BuildPatchProfileRequest()
    {
        return new PatchProfileRequest() {
            Name = "Jane Doe",
            ContactDetails = new RedacteurPortaal.Api.Models.Profile.ContactDetails() {
                Address = "foo1",
                City = "bar1",
                Email = "foo1@bar.nl",
                PhoneNumber = "0640778811",
                Province = "foo1",
                PostalCode = "9999BB"
            },
            ProfilePicture = "base65"
        };
    }

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
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "ZipCode" },
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
            ArchiveMaterial = "",
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
            DurationSeconds = 1,
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
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "ZipCode" },
            Format = "Format 1",
            MediaLocation = new Uri("http://www.google.com"),
            Weather = RedacteurPortaal.DomainModels.Media.Weather.SUNNY,
        };
    }

    public static MediaPhotoItemDto CreateMediaPhotoItemRequest(){
        return new MediaPhotoItemDto() {
            Title = "Photo 1",
            Folder = "Folder 1",
            RepublishDate = DateTime.Now,
            Rights = "Rights 1",
            Camera = "Camera 1",
            LastWords = "Last Words 1",
            ProxyFile = "ProxyFile 1",
            Presentation = "Presentation 1",
            Location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location 1", City = "City 1", Province = "Province 1", Latitude = 1, Longitude = 1, Street = "Street", Zip = "ZipCode" },
            Format = "Format 1",
            MediaLocation = new Uri("http://www.google.com"),
            Image = "Image 1",
        };
    }
}