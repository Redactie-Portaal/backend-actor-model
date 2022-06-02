using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests.DomainModels;

[TestClass]
public class MediaVideoItemValidationTests
{
    [TestMethod]
    public void CanCreateMediaVideoItem()
    {
        var guid = Guid.NewGuid();
        try
        {

        var mediaAudioItem = new MediaVideoItem(guid,
                                                                       "Title",
                                                                       "Folder",
                                                                       DateTime.UtcNow,
                                                                       "Rights",
                                                                       "Camera",
                                                                       "Lastwords",
                                                                       "Proxyfile",
                                                                       "Presentation",
                                                                       new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                                                       "Format",
                                                                       "Reporter",
                                                                       "Sound",
                                                                       "Editor",
                                                                       "Lastpicture",
                                                                       new List<string> { "keyword" },
                                                                       "Voiceover",
                                                                       "Description",
                                                                       DateTime.UtcNow,
                                                                       "Itemname",
                                                                       "Epg",
                                                                       TimeSpan.FromSeconds(10),
                                                                       "Archivematerial",
                                                                       Weather.SUNNY,
                                                                       "Producer",
                                                                       "Director",
                                                                       new List<string> { "guest" },
                                                                       "Firstpicture",
                                                                       "Programname",
                                                                       "Firstwords",
                                                                       new Uri("https://microsoft.com"));
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
            var model = new MediaVideoItem(guid,
                                           "",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.FromSeconds(10),
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyFolder()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyRights()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyCamera()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyLastwords()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyProxyFile()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyPresentation()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }
    [TestMethod]
    public void ThrowsWithEmptyLocation()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyFormat()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyReporter()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptySound()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyEditory()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyLastPicture()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyKeywords()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyVoiceOver()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyDescription()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyItemname()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyEpg()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyArchivematerial()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyProducer()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyDirector()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyGuests()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { },
                                           "Firstpicture",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyFirstpicture()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "",
                                           "Programname",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyProgramname()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "",
                                           "Firstwords",
                                           new Uri("https://microsoft.com"));
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyFirstwords()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaVideoItem(guid,
                                           "Title",
                                           "Folder",
                                           DateTime.UtcNow,
                                           "Rights",
                                           "Camera",
                                           "Lastwords",
                                           "Proxyfile",
                                           "Presentation",
                                           new Location(guid, "Name", "City", "Province", "Street", "1000AB", 0, 90),
                                           "Format",
                                           "Reporter",
                                           "Sound",
                                           "Editor",
                                           "Lastpicture",
                                           new List<string> { "keyword" },
                                           "Voiceover",
                                           "Description",
                                           DateTime.UtcNow,
                                           "Itemname",
                                           "Epg",
                                           TimeSpan.Zero,
                                           "Archivematerial",
                                           Weather.SUNNY,
                                           "Producer",
                                           "Director",
                                           new List<string> { "guest" },
                                           "Firstpicture",
                                           "Programname",
                                           "",
                                           new Uri("https://microsoft.com"));
        });

    }

}        
