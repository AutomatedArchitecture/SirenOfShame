using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.UI
{
    [TestClass]
    public class NewsFeedTest
    {
        [TestMethod]
        public void NullFiltersGetEverything()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "sam" } }, null, null));
        }
        
        [TestMethod]
        public void PeopleFilters()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "bob" } }, "bob", null));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { Person = new PersonSetting { RawName = "bob"}}, "sam", null));
        }

        [TestMethod]
        public void BuildFilters()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildStarted, BuildId = "22" }, null, "22"));
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildFailed, BuildId = "22" }, null, "22"));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildStarted, BuildId = "23" }, null, "22"));
            Assert.IsFalse(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.BuildFailed, BuildId = "23" }, null, "22"));
        }
        
        [TestMethod]
        public void BuildFiltersStillGetSosNews()
        {
            Assert.IsTrue(NewsFeed.IncludeInFilter(new NewNewsItemEventArgs { NewsItemType = NewsItemTypeEnum.SosOnlineComment, BuildId = null}, null, "22"));
        }
    }
}
