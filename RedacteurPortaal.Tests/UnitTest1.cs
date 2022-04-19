using Moq;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.Grains;
using RedacteurPortaal.Grains.GrainServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests
{
    [CollectionDefinition("Col")]
    public class ClusterCollection : ICollectionFixture<ClusterFixture>
    {

    }

    [Collection("Col")]
    public class UnitTest1
    {
        private readonly TestCluster _cluster;

        public UnitTest1(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }

        [Fact]
        public async Task Test1Async()
        {
            var guid = Guid.NewGuid();

            var newsitem = new NewsItemModel(guid, "Newsitem Title", Status.DONE, "Newsitem Author", new FeedSource { }, new ItemBody { }, new List<Contact>(), new Location { }, DateTime.UtcNow, DateTime.UtcNow, Category.STORY, Region.LOCAL, new MediaVideoItem[0], new MediaAudioItem[0], new MediaPhotoItem[0]);


            var newsitemgrain = this._cluster.GrainFactory.GetGrain<INewsItemGrain>(guid);

            await newsitemgrain.AddNewsItem(newsitem);

            var news = ClusterFixture.GrainStorage.GetGrainState<NewsItemModel>(newsitemgrain);

            Assert.Equal("abc", news.Title);
            Assert.Equal(Status.DONE, news.Status);
        }

        [Fact]
        public async Task Test2Async()
        {
            var guid = Guid.NewGuid();

            var newsitem = new NewsItemModel(guid, "Newsitem Title", Status.DONE, "Newsitem Author", new FeedSource { }, new ItemBody { }, new List<Contact>(), new Location { }, DateTime.UtcNow, DateTime.UtcNow, Category.STORY, Region.LOCAL, new MediaVideoItem[0], new MediaAudioItem[0], new MediaPhotoItem[0]);


            var test = this._cluster.Client.GetGrain<INewsItemGrain>(guid);
            var newsitemgrain = this._cluster.GrainFactory.GetGrain<INewsItemGrain>(guid);

            await test.AddNewsItem(newsitem);

            var news = ClusterFixture.GrainStorage.GetGrainState<NewsItemModel>(newsitemgrain);


            Assert.Equal("Newsitem Title", news.Title);
        }
    }
}