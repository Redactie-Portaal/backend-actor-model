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
    public static NewsItemDto BuildAddNewsItemRequest()
    {
        return new NewsItemDto()
        {
            Author = "Author",
            Audio = new(),
            Body = "foo",
            Category = Category.NEWS,
            Region = Region.LOCAL,
            Photos = new(),
            Videos = new(),
            Source = new FeedSourceDto()
            {
                PlaceHolder = "foo",
            },
            Status = Status.DONE,
            Title = "title",
            ApprovalStatus = "APPROVED",
            ContactDetails = new List<ContactDto>(),
            EndDate = DateTime.Now,
            LocationDetails = new LocationDto()
            {
                City = "foo",
                Latitude = 50,
                Longitude = 50,
                Name = "foo",
                Province = "foo",
                Street = "foo",
                Zip = "5087BB",
            },
            ProductionDate = DateTime.Now,
        };
    }
    public static UpdateNewsItemRequest BuildUpdateNewsItemRequest()
    {
        return new UpdateNewsItemRequest()
        {
            Author = "Author1",
            Audio = new UpdateMediaAudioItemRequest[1],
            Body = "foo1",
            Category = Category.NEWS,
            Region = Region.LOCAL,
            Photos = new UpdateMediaPhotoItemRequest[1],
            Videos = new UpdateMediaVideoItemRequest[1],
            Source = new FeedSourceDto()
            {
                PlaceHolder = "foo1",
            },
            Status = Status.DONE,
            Title = "title1",
            ContactDetails = new List<UpdateContactRequest>(),
            EndDate = DateTime.Now,
            LocationDetails = new UpdateLocationRequest()
            {
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
        return new AddProfileRequest()
        {
            Role = Role.ADMIN,
            FullName = "John Doe",
            ContactDetails = new RedacteurPortaal.Api.Models.Profile.ContactDetails()
            {
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

    public static AddressDTO BuildGetAddressRequest()
    {
        return new AddressDTO() {
            Id = Guid.NewGuid(),
            Address = "FooStreet",
            Webpage = "www.foo.nl",
            CompanyName = "FooBar",
            EmailAddress = "foo@gmail.com",
            PhoneNumber = "0640778812",
            PostalCode = "5087BB"
        };
    }

    public static AddAddressRequest BuildAddAddressRequest()
    {
        return new AddAddressRequest()
        {
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
        return new UpdateAddressRequest()
        {
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
        return new PatchProfileRequest()
        {
            Name = "Jane Doe",
            ContactDetails = new RedacteurPortaal.Api.Models.Profile.ContactDetails()
            {
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

    public static UpdateAgendaRequest BuildPatchAgendaRequest()
    {
        return new UpdateAgendaRequest {
            StartDate = new DateTime(2022, 05, 12, 12, 00, 00),
            EndDate = new DateTime(2022, 05, 12, 14, 00, 00),
            Title = "foo1",
            Description = "bar1"
        };
    }

    public static AgendaDto BuildAgendaRequest()
    {
        return new AgendaDto {
            StartDate = new DateTime(2022, 05, 12, 12, 00, 00),
            EndDate = new DateTime(2022, 05, 12, 14, 00, 00),
            Title = "foo1",
            Description = "bar1",
            UserId = "string"
        };
    }
}