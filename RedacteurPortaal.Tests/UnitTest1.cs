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
        public void Test1()
        {
            var hello = "hi";
            Assert.Equal("hi", hello);
        }

        [Fact]
        public async Task Test2Async()
        {
            //cluster.Deploy();

            var guid = Guid.NewGuid();

            var newsitem = new NewsItemModel(guid, "Newsitem Title", Status.DONE, "Newsitem Author", new FeedSource { }, new ItemBody { }, new List<Contact>(), new Location { }, DateTime.UtcNow, DateTime.UtcNow, Category.STORY, Region.LOCAL, new MediaVideoItem[0], new MediaAudioItem[0], new MediaPhotoItem[0]);

            var newsitemgrain = this._cluster.GrainFactory.GetGrain<INewsItemGrain>(guid);

            await newsitemgrain.AddNewsItem(newsitem);
            var identity = newsitemgrain.GetGrainIdentity();
            var news = await newsitemgrain.Get();


            //Assert.Equal("Newsitem Title", news.Title);


            //var inewsitem = new Mock<INewsItemGrain>();
            //var newsitem = new Mock<NewsItemGrain>();


            //newsitem.Setup(x => x.GrainFactory.GetGrain<INewsItemGrain>)
        }
    }
}