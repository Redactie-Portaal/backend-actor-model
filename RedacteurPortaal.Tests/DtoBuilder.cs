using System;
using System.Collections.Generic;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Profile;

namespace RedacteurPortaal.Tests;

public static class DtoBuilder
{
    public static ArchiveDto BuildAddArchiveRequest()
    {
        return new ArchiveDto() {
            Id = Guid.NewGuid(),
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

    public static ArchiveDto BuildUpdateArchiveRequest()
    {
        return new ArchiveDto() {
            Id = Guid.NewGuid(),
            Title = "New Title1",
            Label = "New Label1",
            MediaPhotoItems = new(),
            MediaVideoItems = new(),
            MediaAudioItems = new(),
            NewsItems = new(),
            ArchivedDate = DateTime.Now,
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
}