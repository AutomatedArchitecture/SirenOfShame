using SirenOfShame.Lib.Settings;
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
        public void GetPerson_PersonWithRawNameDoesNotExist_NothingFound()
        {
            var settingsFake = new SirenOfShameSettingsFake();
            var actual = NewNewsItemEventArgs.GetPerson(settingsFake, "domain\richardsonl");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetPerson_PersonWithRawNameExists_PersonIsFound()
        {
            var settingsFake = new SirenOfShameSettingsFake();
            settingsFake.People.Add(new PersonSetting { RawName = "domain\richardsonl"});
            var actual = NewNewsItemEventArgs.GetPerson(settingsFake, "domain\richardsonl");
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GetPerson_NameContainsCommaButIsSearchedForWithoutComma_NameStillFound()
        {
            var settingsFake = new SirenOfShameSettingsFake();
            settingsFake.People.Add(new PersonSetting { RawName = "O'Boyle, Robert"});
            var actual = NewNewsItemEventArgs.GetPerson(settingsFake, "O'Boyle  Robert");
            Assert.IsNotNull(actual);
        }

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
                BuildDefinitionId = "Project 1",
                ReputationChange = -1,
                BuildId = "5",
            };
            var asCommaSeparated = args.AsCommaSeparated();
            var result = NewNewsItemEventArgs.FromCommaSeparated(asCommaSeparated, settings);
            Assert.IsNotNull(result);
            Assert.AreEqual("Bob", result.Person.RawName);
            Assert.AreEqual("Hello world", result.Title);
            Assert.AreEqual(new DateTime(2010, 1, 2, 3, 4, 5, 6), result.EventDate);
            Assert.AreEqual(NewsItemTypeEnum.BuildFailed, result.NewsItemType);
            Assert.AreEqual(-1, result.ReputationChange);
            Assert.AreEqual("Project 1", result.BuildDefinitionId);
            Assert.AreEqual("5", result.BuildId);
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
                Title = ",Hello, world,",
                BuildDefinitionId = "Project 1"
            };
            var asCommaSeparated = args.AsCommaSeparated();
            var result = NewNewsItemEventArgs.FromCommaSeparated(asCommaSeparated, settings);
            Assert.IsNotNull(result);
            Assert.AreEqual(",Hello, world,", result.Title);
        }
        
        [TestMethod]
        public void SerializeThenDeserialize_NewLineInTitle()
        {
            SirenOfShameSettingsFake settings = new SirenOfShameSettingsFake();
            var person = settings.FindAddPerson("Bob");
            NewNewsItemEventArgs args = new NewNewsItemEventArgs
            {
                EventDate = new DateTime(2010, 1, 2, 3, 4, 5, 6),
                Person = person,
                NewsItemType = NewsItemTypeEnum.BuildFailed,
                Title = @"Hello
world",
                BuildDefinitionId = "Project 1"
            };
            var asCommaSeparated = args.AsCommaSeparated();
            var result = NewNewsItemEventArgs.FromCommaSeparated(asCommaSeparated, settings);
            Assert.IsNotNull(result);
            Assert.AreEqual("Hello world", result.Title);
        }
    }
}
