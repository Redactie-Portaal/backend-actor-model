﻿using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace RedacteurPortaal.Tests.DomainModels;

public class NewsItemModelValidationTests
{
    [Fact]
    public void NewsItemModelCorrect()
    {
        var guid = Guid.NewGuid();
        var exc = Record.Exception(() => new NewsItemModel(guid,
                                         "Newsitem Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource { },
                                         "body",
                                         new List<Contact> { new Contact(guid, "name", "email@email.com", "0612345678") },
                                         new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                         DateTime.UtcNow,
                                         DateTime.UtcNow,
                                         Category.STORY,
                                         Region.LOCAL,
                                         new List<MediaVideoItem>(),
                                         new List<MediaAudioItem>(),
                                         new List<MediaPhotoItem>()));

        Assert.Null(exc);
    }

    [Fact]
    public void ThrowsWithIncorrectContact()
    {
        var guid = Guid.NewGuid();

        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "Newsitem Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource { },
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

    [Fact]
    public void ThrowsWithEmptyTitle()
    {
        var guid = Guid.NewGuid();

        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Newsitem Author",
                                         new FeedSource { },
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
            ;
        });
    }

    [Fact]
    public void ThrowsWithEmptyAuthor()
    {
        var guid = Guid.NewGuid();

        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "",
                                         new FeedSource { },
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

    [Fact]
    public void ThrowsWithEmptyBody()
    {
        var guid = Guid.NewGuid();

        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource { },
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

    [Fact]
    public void ThrowsWithEmptyOrWrongLocation()
    {
        var guid = Guid.NewGuid();

        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource { },
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
            ;
        });
        Assert.Throws<ValidationException>(() => {
            var model = new NewsItemModel(guid,
                                         "Title",
                                         Status.DONE,
                                         ApprovalState.PENDING,
                                         "Author",
                                         new FeedSource { },
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
            ;
        });
    }
}
