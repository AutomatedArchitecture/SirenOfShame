using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.UI
{
    [TestFixture]
    public class NewsFeedTest
    {
        [Test]
        public void NullFiltersGetEverything()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "sam" } }, null, null));
        }
        
        [Test]
        public void PeopleFilters()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "bob" } }, "bob", null));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "bob"}}, "sam", null));
        }

        [Test]
        public void BuildFilters()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildStarted, BuildDefinitionId = "22" }, null, "22"));
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildFailed, BuildDefinitionId = "22" }, null, "22"));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildStarted, BuildDefinitionId = "23" }, null, "22"));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildFailed, BuildDefinitionId = "23" }, null, "22"));
        }
        
        [Test]
        public void BuildFiltersStillGetSosNews()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.SosOnlineComment, BuildDefinitionId = null }, null, "22"));
        }
    }
}
