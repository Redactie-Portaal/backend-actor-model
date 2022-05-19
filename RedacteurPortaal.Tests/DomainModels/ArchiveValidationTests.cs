using FluentValidation;
using RedacteurPortaal.DomainModels.Archive;
using RedacteurPortaal.DomainModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.DomainModels
{
    public class ArchiveValidationTests
    {
        [Fact]
        public void CanCreateArchive()
        {
            var guid = Guid.NewGuid();
            var mediaAudioItem = Record.Exception(() => new ArchiveModel(guid,
                                                                         "Title",
                                                                         "Label",
                                                                         new List<MediaPhotoItem>(),
                                                                         new List<MediaVideoItem>(),
                                                                         new List<MediaAudioItem>(),
                                                                         DateTime.UtcNow,
                                                                         new List<string> { "scripts" }));
            Assert.Null(mediaAudioItem);
        }

        [Fact]
        public void ThrowsWithEmptyTitle()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
                var model = new ArchiveModel(guid,
                                             "",
                                             "Label",
                                             new List<MediaPhotoItem>(),
                                             new List<MediaVideoItem>(),
                                             new List<MediaAudioItem>(),
                                             DateTime.UtcNow,
                                             new List<string> { "scripts" });
            });
        }
        [Fact]
        public void ThrowsWithEmptyLabel()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
                var model = new ArchiveModel(guid,
                                             "Title",
                                             "",
                                             new List<MediaPhotoItem>(),
                                             new List<MediaVideoItem>(),
                                             new List<MediaAudioItem>(),
                                             DateTime.UtcNow,
                                             new List<string> { "scripts" });
            });
        }
        [Fact]
        public void ThrowsWithEmptyScripts()
        {
            var guid = Guid.NewGuid();

            Assert.Throws<ValidationException>(() => {
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
}
