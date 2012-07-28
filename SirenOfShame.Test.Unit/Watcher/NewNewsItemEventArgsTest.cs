// ReSharper disable InconsistentNaming
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestClass]
    public class NewNewsItemEventArgsTest
    {
        [TestMethod]
        public void SerializeThenDeserialize()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            var person = settings.FindAddPerson("Bob");
            NewNewsItemEventArgs args = new NewNewsItemEventArgs
            {
                EventDate = new DateTime(2010, 1, 2, 3, 4, 5, 6),
                Person = person,
                Title = "Hello world",
                NewsItemType = NewsItemTypeEnum.BuildFailed,
                ReputationChange = -1,
            };
            var asCommaSeparated = args.AsCommaSeparated();
            var result = NewNewsItemEventArgs.FromCommaSeparated(asCommaSeparated, settings);
            Assert.IsNotNull(result);
            Assert.AreEqual("Bob", result.Person.RawName);
            Assert.AreEqual("Hello world", result.Title);
            Assert.AreEqual(new DateTime(2010, 1, 2, 3, 4, 5, 6), result.EventDate);
            Assert.AreEqual(NewsItemTypeEnum.BuildFailed, result.NewsItemType);
            Assert.AreEqual(-1, result.ReputationChange);
        }
        
        [TestMethod]
        public void SerializeThenDeserialize_CommaSeparatedTitle()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            var person = settings.FindAddPerson("Bob");
            NewNewsItemEventArgs args = new NewNewsItemEventArgs
            {
                EventDate = new DateTime(2010, 1, 2, 3, 4, 5, 6),
                Person = person,
                NewsItemType = NewsItemTypeEnum.BuildFailed,
                Title = ",Hello, world,"
            };
            var asCommaSeparated = args.AsCommaSeparated();
            var result = NewNewsItemEventArgs.FromCommaSeparated(asCommaSeparated, settings);
            Assert.IsNotNull(result);
            Assert.AreEqual(",Hello, world,", result.Title);
        }
    }
}
