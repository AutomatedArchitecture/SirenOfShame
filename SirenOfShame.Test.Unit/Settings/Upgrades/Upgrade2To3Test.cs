﻿// ReSharper disable InconsistentNaming

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Settings.Upgrades;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Settings.Upgrades
{
    [TestClass]
    public class Upgrade2To3Test
    {
        [TestMethod]
        public void Upgrade_BuildTriggeredRule_WindowsAudioSetToPlunk()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered
            });
            new Upgrade2To3().Upgrade(settings);
            Assert.AreEqual(1, settings.Rules.Count);
            Assert.AreEqual("SirenOfShame.Resources.Audio-Plunk.wav", settings.Rules[0].WindowsAudioLocation);
        }
        
        [TestMethod]
        public void Upgrade_BuildFailedRule_WindowsAudioSetToSadTrombone()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed
            });
            settings.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild
            });
            new Upgrade2To3().Upgrade(settings);
            Assert.AreEqual(2, settings.Rules.Count);
            Assert.IsTrue(settings.Rules.All(i => i.WindowsAudioLocation == "SirenOfShame.Resources.Audio-Sad-Trombone.wav"));
        }
        
        [TestMethod]
        public void Upgrade_SubsequentBuildFailedRule_WindowsAudioSetToSadTrombone()
        {
            var settings = new SirenOfShameSettingsFake
                               {
                                   Version = null
                               };
            settings.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SubsequentFailedBuild
            });
            new Upgrade2To3().Upgrade(settings);
            Assert.AreEqual(1, settings.Rules.Count);
            Assert.AreEqual("SirenOfShame.Resources.Audio-Boo-Hiss.wav", settings.Rules[0].WindowsAudioLocation);
        }
    }
}
