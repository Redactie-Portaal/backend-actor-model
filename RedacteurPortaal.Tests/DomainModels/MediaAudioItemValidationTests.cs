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
public class MediaAudioItemValidationTests
{
    [TestMethod]
    public void CanCreateMediaAudioItem()
    {
        var guid = Guid.NewGuid();
        try
        {

        var mediaAudioItem =  new MediaAudioItem(guid,
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
                                                 new Uri("https://microsoft.com"),
                                                 TimeSpan.Zero,
                                                 Weather.SUNNY,
                                                 "Firstwords",
                                                 "Programname");
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
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
            });
    }

    [TestMethod]
    public void ThrowsWithEmptyFolder()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
            });
    }

    [TestMethod]
    public void ThrowsWithEmptyRights()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
            });
    }

    [TestMethod]
    public void ThrowsWithEmptyCamera()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyLastwords()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyProxyFile()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyPresentation()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }
    [TestMethod]
    public void ThrowsWithEmptyLocation()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyFormat()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyUri()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<UriFormatException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri(""),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "Programname");
        });

    }

    [TestMethod]
    public void ThrowsWithEmptyFirstwords()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "",
                                           "Programname");
        });
    }

    [TestMethod]
    public void ThrowsWithEmptyProgram()
    {
        var guid = Guid.NewGuid();

        Assert.ThrowsException<ValidationException>(() => {
            var model = new MediaAudioItem(guid,
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
                                           new Uri("https://microsoft.com"),
                                           TimeSpan.Zero,
                                           Weather.SUNNY,
                                           "Firstwords",
                                           "");
        });

    }
}
