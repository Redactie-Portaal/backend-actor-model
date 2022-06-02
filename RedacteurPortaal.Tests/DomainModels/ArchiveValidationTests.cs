using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;

namespace RedacteurPortaal.Tests.DomainModels;

[TestClass]
public class ArchiveValidationTests
{
    [TestMethod]
    public void CanCreateArchive()
    {
        var guid = Guid.NewGuid();
        try
        {
            var mediaAudioItem = new ArchiveModel(guid,
                "Title",
                "Label",
                new List<MediaPhotoItem>(),
                new List<MediaVideoItem>(),
                new List<MediaAudioItem>(),
                DateTime.UtcNow,
                new List<string> {"scripts"});
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }
    }
    
    [TestMethod]
    public void ThrowsWithEmptyTitle()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var model = new ArchiveModel(guid,
                "",
                "Label",
                new List<MediaPhotoItem>(),
                new List<MediaVideoItem>(),
                new List<MediaAudioItem>(),
                DateTime.UtcNow,
                new List<string> {"scripts"});
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyLabel()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new ArchiveModel(guid,
                "Title",
                "",
                new List<MediaPhotoItem>(),
                new List<MediaVideoItem>(),
                new List<MediaAudioItem>(),
                DateTime.UtcNow,
                new List<string> {"scripts"});
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyScripts()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new ArchiveModel(guid,
                "Title",
                "Label",
                new List<MediaPhotoItem>(),
                new List<MediaVideoItem>(),
                new List<MediaAudioItem>(),
                DateTime.UtcNow,
                new List<string>());
        });
    }
}