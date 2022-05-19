using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.Grains.Test;

[Collection("Col")]
public class NewsItemGrainTests
{
    private readonly TestCluster _cluster;

    public NewsItemGrainTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }

    [Fact]
    public async Task CanAddNewsItemCorrectly()
    {
        var guid = Guid.NewGuid();

        var newsitem = new NewsItemModel(guid,
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
                                         new(),
                                         new(),
                                         new());


        var newsitemgrain = this._cluster.GrainFactory.GetGrain<INewsItemGrain>(guid);

        await newsitemgrain.Update(newsitem);

        var news = await newsitemgrain.Get();

        Assert.Equal("Newsitem Title", news.Title);
        Assert.Equal(Status.DONE, news.Status);
    }

}