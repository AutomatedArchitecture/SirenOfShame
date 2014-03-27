// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using NUnit.Framework;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Settings.Upgrades;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings.Upgrades
{
    [TestFixture]
    public class Upgrade5To6Test
    {
        [Test]
        public void Upgrade_ThreePeopleTwoAvatarImages_LastPersonsAvatarIdLoopsBackToZero()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null,
                                   People = new List<PersonSetting>
                                   {
                                       new PersonSetting { RawName = "1"},
                                       new PersonSetting { RawName = "2"},
                                       new PersonSetting { RawName = "3"},
                                   }
                               };
            settings.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered
            });
            new Upgrade5To6(2).Upgrade(settings);
            Assert.AreEqual(3, settings.People.Count);
            Assert.AreEqual(0, settings.People[0].AvatarId);
            Assert.AreEqual(1, settings.People[1].AvatarId);
            Assert.AreEqual(0, settings.People[2].AvatarId);
        }
    }
}
