using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Moq;
using RedacteurPortaal.Api;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.Data.Models;
using RedacteurPortaal.Helpers;
using Xunit;

namespace RedacteurPortaal.Tests.Api;

[Collection("Col")]
public class ExportPluginServiceTests
{
    [Fact]
    public void EmptyListWhenNoPlugins()
    {
        var dbContext = new Mock<DataContext>();
        var fileSystemProvider = new Mock<FileSystemProvider>();
        var plugins = new List<PluginSettings>();

        dbContext.Setup(x => x.PluginSettings).Returns(plugins.GetQueryableMockDbSet().Object);
        fileSystemProvider.Setup(x => x.FileSystem.Directory.GetFiles(It.IsAny<string>(), "*.dll")).Returns(System.Array.Empty<string>());

        var service = new ExportPluginService(dbContext.Object, fileSystemProvider.Object);
        var result = service.GetPlugins();

        // Verify that it was not added to the db.
        dbContext.Verify(x => x.PluginSettings.Add(It.IsAny<PluginSettings>()), Times.Never);

        Assert.Empty(result);
    }
}