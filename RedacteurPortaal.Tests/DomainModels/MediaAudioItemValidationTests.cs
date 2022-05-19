using FluentValidation;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.DomainModels
{
    public class MediaAudioItemValidationTests
    {
        [Fact]
        public void CanCreateMediaAudioItem()
        {
            var guid = Guid.NewGuid();
            var mediaAudioItem = Record.Exception(() => new MediaAudioItem(guid,
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
                                                                           "Programname"));
            Assert.Null(mediaAudioItem);
        }

        [Fact]
        public void ThrowsWithEmptyTitle()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyFolder()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyRights()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyCamera()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyLastwords()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyProxyFile()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyPresentation()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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
        [Fact]
        public void ThrowsWithEmptyLocation()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyFormat()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyUri()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<UriFormatException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyFirstwords()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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

        [Fact]
        public void ThrowsWithEmptyProgram()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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
}
