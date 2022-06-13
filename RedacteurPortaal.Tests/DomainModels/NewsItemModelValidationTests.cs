using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;

namespace RedacteurPortaal.Tests.DomainModels;

[TestClass]
public class NewsItemModelValidationTests
{
    [TestMethod]
    public void NewsItemModelCorrect()
    {
        var guid = Guid.NewGuid();
        try
        {

            _ = new NewsItemModel(guid,
                                         "Newsitem Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }
    }

    [TestMethod]
    public void ThrowsWithIncorrectContact()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "Newsitem Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "Mail", "Phone") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyTitle()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyAuthor()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyBody()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource(),
                                         "",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyOrWrongLocation()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
        Assert.ThrowsException<ValidationException>(() => {
            _ = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource(),
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "postcode", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>());
        });
    }
}