using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orleans;
using Orleans.TestingHost;
using RedacteurPortaal.Data.Context;
using RedacteurPortaal.Data.Models;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Tests.Grains.GrainServices;

[TestClass]
public class GrainManagementServiceTests
{
    private TestCluster testCluster;

    [TestInitialize]
    public void Initialize()
    {
        this.testCluster = new ClusterFixture().Cluster;
    }

    [TestMethod]
    public async Task ThrowsWhenIdExists()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();

        var references = new List<GrainReference>() {
            new GrainReference() {
                GrainId = id,
                TypeName = typeof(SourceGrain).Name,
            }
        };

        dbContext.Setup(x => x.GrainReferences).Returns(references.GetQueryableMockDbSet().Object);

        var clusterClient = new Mock<IClusterClient>();

        var logger = new Mock<ILogger<GrainManagementService<SourceGrain, Source>>>();
        
        var service = new GrainManagementService<SourceGrain, Source>(dbContext.Object, clusterClient.Object, logger.Object);

        await Assert.ThrowsExceptionAsync<DuplicateNameException>(async () => {
            _ = await service.CreateGrain(id);
        });
    }

    [TestMethod]
    public async Task GetThrowsWhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();

        var references = new List<GrainReference>();

        dbContext.Setup(x => x.GrainReferences).Returns(references.GetQueryableMockDbSet().Object);

        var clusterClient = new Mock<IClusterClient>();

        var logger = new Mock<ILogger<GrainManagementService<SourceGrain, Source>>>();            

        var service = new GrainManagementService<SourceGrain, Source>(dbContext.Object, clusterClient.Object, logger.Object);

        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => {
            _  = await service.GetGrain(id);
        });
    }

    [TestMethod]
    public void GetReturns()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();

        var references = new List<GrainReference>() {
            new GrainReference() {
                GrainId = id,
                TypeName = typeof(SourceGrain).Name,
            }
        };

        dbContext.Setup(x => x.GrainReferences).Returns(references.GetQueryableMockDbSet().Object);

        var logger = new Mock<ILogger<GrainManagementService<SourceGrain, Source>>>();

        var service = new GrainManagementService<SourceGrain, Source>(dbContext.Object, this.testCluster.Client, logger.Object);

        Assert.IsNotNull(service.GetGrain(id));
    }

    [TestMethod]
    public async Task CreatedNewWhenIdNotTaken()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();
        var gr = new GrainReference() {GrainId = id, TypeName = typeof(ContactGrain).Name};
        var references = new List<GrainReference>();

        dbContext.Setup(x => x.GrainReferences).Returns(references.GetQueryableMockDbSet().Object);
        dbContext.Setup(x => x.GrainReferences.Add(It.IsAny<GrainReference>())).Callback(() =>
            references.Add(new GrainReference() { GrainId = id, TypeName = typeof(ContactGrain).Name }));

        var logger = new Mock<ILogger<GrainManagementService<IContactGrain, Contact>>>();

        var service = new GrainManagementService<IContactGrain, Contact>(dbContext.Object, this.testCluster.Client, logger.Object);

        var foo = await (await service.CreateGrain(id)).Get();

        Assert.IsTrue(foo.Name == null);
        //CollectionAssert.Contains(references, x => x.GrainId == gr.GrainId && x.TypeName == gr.TypeName);

        var t = new List<GrainReference>();
        t.Add(gr);

        CollectionAssert.Equals(t, references);
    }


    [TestMethod]
    public async Task DeleteDeletes()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();

        var references = new List<GrainReference>() {
            new GrainReference() {
                GrainId = id,
                TypeName = typeof(IContactGrain).Name,
            }
        };
        var dbset = references.GetQueryableMockDbSet();
        dbset.Setup(x => x.Remove(It.IsAny<GrainReference>())).Callback(() => references.RemoveAt(0));

        var logger = new Mock<ILogger<GrainManagementService<IContactGrain, Contact>>>();

        var service = new GrainManagementService<IContactGrain, Contact>(dbContext.Object, this.testCluster.Client, logger.Object);
        dbContext.Setup(x => x.GrainReferences).Returns(dbset.Object);

        await service.DeleteGrain(id);

        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => {
            _ = await service.GetGrain(id);
        });
    }

    [TestMethod]
    public async Task DeleteThrowsIfKeyNotExists()
    {
        var id = Guid.NewGuid();
        var dbContext = new Moq.Mock<DataContext>();

        var references = new List<GrainReference>();
        dbContext.Setup(x => x.GrainReferences).Returns(references.GetQueryableMockDbSet().Object);

        var logger = new Mock<ILogger<GrainManagementService<IContactGrain, Contact>>>();

        var service = new GrainManagementService<IContactGrain, Contact>(dbContext.Object, this.testCluster.Client, logger.Object);


        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => {
            await service.DeleteGrain(id);
        });
    }
}
