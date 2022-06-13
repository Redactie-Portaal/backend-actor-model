using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
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
                                                  new List<Guid>(),
                                                  new List<Guid>(),
                                                  new List<Guid>(),
                                                  new List<Guid>(),
                                                  DateTime.UtcNow,
                                                  new List<string> { "scripts" });
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }

        //Assert.(mediaAudioItem);
    }


    [TestMethod]
    public void ThrowsWithEmptyTitle()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var model = new ArchiveModel(guid,
                                         "Title",
                                         "",
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         DateTime.UtcNow,
                                         new List<string> { "scripts" });
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
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         new List<Guid>(),
                                         DateTime.UtcNow,
                                         new List<string>());
        });
    }
}