﻿// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    [TestClass]
    public class RulesEngineTest
    {
        [TestMethod]
        public void IdenticalBuildTwice_ShouldNotTriggerTrayIconTheSecondTime()
        {
            var rulesEngine = new RulesEngineWrapper();
            AssertTrayIconCountAndLastColor(rulesEngine.SetTrayIconEvents, 1, TrayIcon.Question);
            var buildStatus = new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.Working,
                    Name = RulesEngineWrapper.BUILD1_ID,
                    RequestedBy = RulesEngineWrapper.CURRENT_USER,
                    BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                    StartedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                    FinishedTime = new DateTime(2010, 1, 1, 1, 10, 10),
                    Comment = "Fixing a typo"
                };
            rulesEngine.InvokeStatusChecked(buildStatus);
            AssertTrayIconCountAndLastColor(rulesEngine.SetTrayIconEvents, 2, TrayIcon.Green);
            rulesEngine.InvokeStatusChecked(buildStatus);
            AssertTrayIconCountAndLastColor(rulesEngine.SetTrayIconEvents, 2, TrayIcon.Green);
        }

        [TestMethod]
        public void BuildPassesThenFails_TrayIconShouldTurnRed()
        {
            var rulesEngine = new RulesEngineWrapper();
            Assert.AreEqual(1, rulesEngine.SetTrayIconEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            AssertTrayIconCountAndLastColor(rulesEngine.SetTrayIconEvents, 2, TrayIcon.Green);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            AssertTrayIconCountAndLastColor(rulesEngine.SetTrayIconEvents, 3, TrayIcon.Red);
        }

        private void AssertTrayIconCountAndLastColor(IList<SetTrayIconEventArgs> trayIcons, int count, TrayIcon trayIcon)
        {
            Assert.AreEqual(trayIcons.Count, count);
            if (trayIcons.Any())
                Assert.AreEqual(trayIcon, trayIcons.Last().TrayIcon);
        }

        [TestMethod]
        public void UserMappingExistsForUser2ToUser1AndUser2ChecksIn_RefreshStatusLooksLikeUser1()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.UserMappings.Add(new UserMapping { WhenISee = "User2", PretendItsActually = RulesEngineWrapper.CURRENT_USER});
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.InProgress,
                Name = "Name",
                RequestedBy = "User2",
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2010, 1, 1, 11, 33, 0),
            });
            RefreshStatusEventArgs lastRefreshStatusEvent = rulesEngine.RefreshStatusEvents.Last();
            Assert.AreEqual(1, lastRefreshStatusEvent.BuildStatusDtos.Count);
            BuildStatusDto buildStatusDto = lastRefreshStatusEvent.BuildStatusDtos[0];
            Assert.AreEqual("User1", buildStatusDto.RequestedByRawName);
        }
        
        [TestMethod]
        public void DuplicateBuildDefinitionIds_BuildDefinitionDisplayNamesGetQualified()
        {
            var rulesEngine = new RulesEngineWrapper();
            var ciEntryPointSetting = rulesEngine.Settings.CiEntryPointSettings[0];
            Assert.AreEqual("http://fake", ciEntryPointSetting.Url);
            var buildDefinitionSetting1 = ciEntryPointSetting.BuildDefinitionSettings[0];
            var buildDefinitionSetting2 = ciEntryPointSetting.BuildDefinitionSettings[1];
            buildDefinitionSetting2.Name = buildDefinitionSetting1.Name;
            rulesEngine.Settings.ClearDuplicateNameCache();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            var refreshStatusEvent = rulesEngine.RefreshStatusEvents.Last();
            Assert.AreEqual(1, refreshStatusEvent.BuildStatusDtos.Count);
            var buildStatusDto = refreshStatusEvent.BuildStatusDtos[0];
            Assert.AreEqual("Build Def 1 (fake)", buildStatusDto.BuildDefinitionDisplayName);
        }

        [TestMethod]
        public void SubsequentBuildStatusRequest_UsesLocalTimeSoXMinuesAgoIsAccurate()
        {
            var rulesEngine = new RulesEngineWrapper();
            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            Assert.AreEqual(3, rulesEngine.RefreshStatusEvents.Count);
            var refreshStatusEvent = rulesEngine.RefreshStatusEvents.Last();
            var buildStatusDto = refreshStatusEvent.BuildStatusDtos.First(i => i.BuildDefinitionId == RulesEngineWrapper.BUILD1_ID);
            Assert.AreNotEqual(new DateTime(2010, 1, 1, 1, 1, 1), buildStatusDto.LocalStartTime);
            Assert.IsTrue((DateTime.Now - buildStatusDto.LocalStartTime).TotalSeconds < 30, "LocalStartTime should have been less than 30 seconds ago aka as close to Now() as possible.");
        }
        
        [TestMethod]
        public void InitialBuildStatusRequest_UsesServerTimeSinceLocalTimeIsNotAvaiable()
        {
            var rulesEngine = new RulesEngineWrapper();
            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            var refreshStatusEvent = rulesEngine.RefreshStatusEvents.Last();
            var buildStatusDto = refreshStatusEvent.BuildStatusDtos.First(i => i.BuildDefinitionId == RulesEngineWrapper.BUILD1_ID);
            Assert.AreEqual(new DateTime(2010, 1, 1, 1, 1, 1), buildStatusDto.LocalStartTime);
        }
        
        [TestMethod]
        public void BuildInitiated_BuildInitiatedNewsItem()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            Assert.AreEqual(1, rulesEngine.NewNewsItemEvents.Count);
            var newNewsItem = rulesEngine.NewNewsItemEvents[0];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, newNewsItem.Person.RawName);
            Assert.AreEqual("'Fixing a typo'", newNewsItem.Title);
        }

        [TestMethod]
        public void BuildWorkingThenWorking_BuildInitiatedNewsItem()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.NewNewsItemEvents.Count);
            var latestNewsItem = rulesEngine.NewNewsItemEvents[1];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, latestNewsItem.Person.RawName);
            Assert.AreEqual("Successful build", latestNewsItem.Title);
        }

        [TestMethod]
        public void BuildFailedThenPassed_BuildInitiatedNewsItem()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.NewNewsItemEvents.Count);
            var newNewsItem = rulesEngine.NewNewsItemEvents[0];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, newNewsItem.Person.RawName);
            Assert.AreEqual("Fixed the broken build", newNewsItem.Title);
        }

        [TestMethod]
        public void BuildFailedThenFailedAgain_BuildInitiatedNewsItem()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(2, rulesEngine.NewNewsItemEvents.Count);
            var latestNewsItem = rulesEngine.NewNewsItemEvents[1];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, latestNewsItem.Person.RawName);
            Assert.AreEqual("Failed to fix the build", latestNewsItem.Title);
        }

        [TestMethod]
        public void UserHas23And59MinutesOfBuildTime_ChecksIn_AchievesTimeWarrior()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.Settings.People.Count);
            rulesEngine.Settings.People[0].CumulativeBuildTime = new TimeSpan(23, 59, 59).Ticks;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.NewAchievementEvents.Count);
            var achievementEventArg = rulesEngine.NewAchievementEvents[0];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, achievementEventArg.Person.RawName);
            Assert.AreEqual(1, achievementEventArg.Achievements.Count);
            Assert.AreEqual(AchievementEnum.TimeWarrior, achievementEventArg.Achievements[0].Id);
        }

        [TestMethod]
        public void UserHas99ReputationButMissedApprentice_SuccessfulChecksIn_AchievesApprenticeAndNeophyteAchievements()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.Settings.People.Count);
            rulesEngine.Settings.People[0].TotalBuilds = 99;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(100, rulesEngine.Settings.People[0].GetReputation());
            Assert.AreEqual(2, rulesEngine.Settings.People[0].Achievements.Count);
            Assert.AreEqual(1, rulesEngine.NewAchievementEvents.Count);
            var achievementEventArg = rulesEngine.NewAchievementEvents[0];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, achievementEventArg.Person.RawName);
            Assert.AreEqual(2, achievementEventArg.Achievements.Count);
            Assert.AreEqual(AchievementEnum.Apprentice, achievementEventArg.Achievements[0].Id);
            Assert.AreEqual(AchievementEnum.Neophyte, achievementEventArg.Achievements[1].Id);
        }

        [TestMethod]
        public void UserHas23Reputation_SuccessfulChecksIn_NoNewAchievement()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.Settings.People.Count);
            rulesEngine.Settings.People[0].TotalBuilds = 23;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(24, rulesEngine.Settings.People[0].GetReputation());
            Assert.AreEqual(0, rulesEngine.Settings.People[0].Achievements.Count);
        }
        
        [TestMethod]
        public void UserHas24Reputation_SuccessfulChecksIn_AchievesApprenticeAchievement()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.Settings.People.Count);
            rulesEngine.Settings.People[0].TotalBuilds = 24;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(25, rulesEngine.Settings.People[0].GetReputation());
            Assert.AreEqual(1, rulesEngine.Settings.People[0].Achievements.Count);
            Assert.AreEqual(1, rulesEngine.NewAchievementEvents.Count);
            var achievementEventArg = rulesEngine.NewAchievementEvents[0];
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER, achievementEventArg.Person.RawName);
            Assert.AreEqual(1, achievementEventArg.Achievements.Count);
            Assert.AreEqual(AchievementEnum.Apprentice, achievementEventArg.Achievements[0].Id);
        }
        
        [TestMethod]
        public void IsBuildingWithBuildTriggeredRuleToStopOnSuccess_BuildSucceeds_LedsStop()
        {
            var rulesEngine = new RulesEngineWrapper();

            var ledPattern = new LedPattern { Id = 2, Name = "Sally" };
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered,
                LedPattern = ledPattern,
                LightsDuration = null,
            });

            Assert.AreEqual(0, rulesEngine.SetLightsEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.SetLightsEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            Assert.AreEqual(1, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual("Sally", rulesEngine.SetLightsEvents[0].LedPattern.Name);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(null, rulesEngine.SetLightsEvents[1].LedPattern);
        }

        [TestMethod]
        public void Hudson_BuildUrlPassesThrough()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.InProgress,
                Name = "New Name!",
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2010, 1, 1, 11, 33, 0),
                Url = "http://win7ci:8081/job/SvnTest/32/",
                BuildId = "32",
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            RefreshStatusEventArgs refreshStatusEventArgs = rulesEngine.RefreshStatusEvents[1];
            Assert.AreEqual(1, refreshStatusEventArgs.BuildStatusDtos.Count());
            Assert.AreEqual("http://win7ci:8081/job/SvnTest/32/", refreshStatusEventArgs.BuildStatusDtos.First().Url);
            Assert.AreEqual("32", refreshStatusEventArgs.BuildStatusDtos.First().BuildId);
        }

        [TestMethod]
        public void GlobalMuteOnBuildFailsWithWindowsAudioRule_NoAudio()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.Mute = true;
            Rule rule = new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                WindowsAudioLocation = "SirenOfShame.Resources.Sad-Trombone.wav"
            };
            rulesEngine.Rules.Add(rule);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.PlayWindowsAudioEvents.Count);
        }

        [TestMethod]
        public void OnStartup_NeverShowWorkingTrayNotifications()
        {
            var rulesEngine = new RulesEngineWrapper();
            Rule rule = new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                AlertType = AlertType.TrayAlert
            };
            rulesEngine.Rules.Add(rule);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void BuildInitiallyIsPassing_NoWindowsAudio()
        {
            var rulesEngine = new RulesEngineWrapper();
            Rule rule = new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                WindowsAudioLocation = "SirenOfShame.Resources.Sad-Trombone.wav"
            };
            rulesEngine.Rules.Add(rule);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.PlayWindowsAudioEvents.Count);
        }

        [TestMethod]
        public void BuildFailsWithWindowsAudioRule_PlayWindowsAudio()
        {
            var rulesEngine = new RulesEngineWrapper();
            Rule rule = new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                WindowsAudioLocation = "SirenOfShame.Resources.Sad-Trombone.wav"
            };
            rulesEngine.Rules.Add(rule);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.PlayWindowsAudioEvents.Count);
            Assert.AreEqual("SirenOfShame.Resources.Sad-Trombone.wav", rulesEngine.PlayWindowsAudioEvents.First().Location);
        }

        [TestMethod]
        public void BuildFailsWithNoRules_NoAudio()
        {
            var rulesEngine = new RulesEngineWrapper();
            Assert.AreEqual(0, rulesEngine.PlayWindowsAudioEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.PlayWindowsAudioEvents.Count);
        }

        [TestMethod]
        public void BuildNameChanges_BuildSettingsNameIsUpdated()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(
                new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.InProgress,
                    Name = "New Name!",
                    RequestedBy = RulesEngineWrapper.CURRENT_USER,
                    BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                    StartedTime = new DateTime(2010, 1, 1, 11, 33, 0)
                });

            BuildDefinitionSetting buildDefinitionSetting = rulesEngine.Settings.CiEntryPointSettings.Single().BuildDefinitionSettings.Single(i => i.Id == RulesEngineWrapper.BUILD1_ID);
            Assert.AreEqual("New Name!", buildDefinitionSetting.Name);
        }

        [TestMethod]
        [Ignore]
        public void TwoBuildsBackToBack_SystemFindsFirstResultForReputation()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.Settings.People[0].GetReputation());
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.InProgress,
                Name = RulesEngineWrapper.BUILD1_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2010, 1, 1, 11, 33, 0)
            });
            Assert.AreEqual(1, rulesEngine.Settings.People[0].GetReputation());
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.InProgress,
                Name = RulesEngineWrapper.BUILD1_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2010, 1, 1, 11, 44, 0)
            });
            Assert.AreEqual(2, rulesEngine.Settings.People[0].GetReputation(), "If two builds a queued and run back to back the system should find the result of the build it missed.");
        }

        [TestMethod]
        public void InitialServerUnavailable_DisconnectedTrayNotificationSent()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
            var trayNotification = rulesEngine.TrayNotificationEvents[0];
            Assert.AreEqual("Build Server Unavailable", trayNotification.Title);
            Assert.AreEqual("The connection will be restored when possible.", trayNotification.TipText);
            Assert.AreEqual(ToolTipIcon.Error, trayNotification.TipIcon);
        }

        [TestMethod]
        public void ServerUnavailableTwice_OnlyOneTrayNotificationSent()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void ServerUnavailableThenBecomesAvailable_ReconnectedTrayNotificationSent()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            rulesEngine.InvokeStatusChecked(new BuildStatus[] { });
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
            var trayNotification = rulesEngine.TrayNotificationEvents[1];
            Assert.AreEqual("Reconnected", trayNotification.Title);
            Assert.AreEqual("Reconnected to server.", trayNotification.TipText);
            Assert.AreEqual(ToolTipIcon.Info, trayNotification.TipIcon);
        }

        [TestMethod]
        public void ServerUnavailableThenAvailableThenUnavailable_TwoUnavailableTrayNotificationSent()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            rulesEngine.InvokeStatusChecked(new BuildStatus[] { });
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            Assert.AreEqual(3, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual(2,
                            rulesEngine.TrayNotificationEvents.Where(tn => tn.Title == "Build Server Unavailable").Count
                                ());
        }

        [TestMethod]
        public void ServerUnavailableThenAvailableTwice_OnlyOneReconnectedTrayNotification()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeServerUnavailable(new ServerUnavailableException("The network is down."));
            rulesEngine.InvokeStatusChecked(new BuildStatus[] { });
            rulesEngine.InvokeStatusChecked(new BuildStatus[] { });
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void InitialStatusChecked_RefreshStatus()
        {
            var rulesEngine = new RulesEngineWrapper();

            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);

            var startedTime = new DateTime(2010, 1, 2, 1, 1, 1);
            var finishedTime = new DateTime(2010, 1, 2, 1, 2, 2);

            rulesEngine.InvokeStatusChecked(new[]
            {
                new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.Working,
                    Name = "Build Def 1",
                    RequestedBy = "User1",
                    BuildDefinitionId = "Build Def 1",
                    StartedTime = startedTime,
                    FinishedTime = finishedTime
                },
            });

            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents[1].BuildStatusDtos.Count());
            BuildStatusDto buildStatus = rulesEngine.RefreshStatusEvents[1].BuildStatusDtos.First();
            Assert.AreEqual((int)BallsEnum.Green, buildStatus.ImageIndex);
            Assert.AreEqual("Build Def 1", buildStatus.BuildDefinitionDisplayName);
            Assert.AreEqual("User1", buildStatus.RequestedByRawName);
            Assert.AreEqual("Build Def 1", buildStatus.BuildDefinitionId);
            Assert.AreEqual(BuildStatus.FormatAsDayMonthTime(startedTime), buildStatus.StartTimeShort);
            Assert.AreEqual("1:01", buildStatus.Duration);
        }

        [TestMethod]
        public void StatusCheckedTwiceWithIdenticalResults_OnlyOneRefreshStatusEvent()
        {
            var rulesEngine = new RulesEngineWrapper();
            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
        }

        [TestMethod]
        public void BuildBreaksWithNoRules_NoNotifications()
        {
            var rulesEngine = new RulesEngineWrapper();

            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual(1, rulesEngine.StatusBarUpdateEvents.Count);

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(4, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual(4, rulesEngine.StatusBarUpdateEvents.Count);
            Assert.AreEqual(0, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual(0, rulesEngine.SetAudioEvents.Count);
            Assert.AreEqual(0, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void BuildInitiallyBrokenWithGlobalModalDialogRule_ModalDialog()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.ModalDialog
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual("Build newly broken by User1 for Build Def 1", rulesEngine.ModalDialogEvents[0].DialogText);
        }

        [TestMethod]
        public void BuildInitiallyWorkingWithGlobalInitialSuccessDialog_NoAlerts()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialSuccess,
                AlertType = AlertType.ModalDialog
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual(0, rulesEngine.SetAudioEvents.Count);
            Assert.AreEqual(0, rulesEngine.SetLightsEvents.Count);
        }

        [TestMethod]
        public void BuildFailsThenSucceedsWithGlobalInitialSuccessDialog_Alert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialSuccess,
                AlertType = AlertType.ModalDialog
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual("Build is passing again for Build Def 1", rulesEngine.ModalDialogEvents[0].DialogText);
        }

        [TestMethod]
        public void BuildFailsThenInProgressThenFailsWithGlobalInitialFailDialog_OnlyOneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.ModalDialog
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual("Build newly broken by User1 for Build Def 1", rulesEngine.ModalDialogEvents[0].DialogText);
        }

        [TestMethod]
        public void BuildSucceedsTwiceAfterFailWithGlobalInitialSuccessDialog_OnlyOneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialSuccess,
                AlertType = AlertType.ModalDialog
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual("Build is passing again for Build Def 1", rulesEngine.ModalDialogEvents[0].DialogText);
        }

        [TestMethod]
        public void BuildInitiallyInProgressWithGlobalBuildTriggeredRule_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered,
                AlertType = AlertType.TrayAlert,
            });

            rulesEngine.InvokeStatusChecked(
                new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.InProgress,
                    Name = RulesEngineWrapper.BUILD1_ID,
                    RequestedBy = RulesEngineWrapper.CURRENT_USER,
                    BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                    StartedTime = new DateTime(2010, 1, 1),
                    Comment = "my comment"
                });
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual("Build In Progress", rulesEngine.TrayNotificationEvents[0].Title);
            Assert.AreEqual("Build triggered by User1 for Build Def 1\r\nmy comment", rulesEngine.TrayNotificationEvents[0].TipText);
            Assert.AreEqual(ToolTipIcon.Info, rulesEngine.TrayNotificationEvents[0].TipIcon);
        }

        [TestMethod]
        public void BuildInProgressTwiceWithGlobalBuildTriggeredRule_OnlyOneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void BuildInProgressThenPassThenInProgressWithGlobalBuildTriggeredRule_TwoAlerts()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildTriggered,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual(rulesEngine.TrayNotificationEvents[0].Title, rulesEngine.TrayNotificationEvents[1].Title);
        }

        [TestMethod]
        public void BuildFailsThenFailsAgainWithGlobalInitialAndSubsequentFailAlerts_TwoAlerts()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.TrayAlert
            });
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SubsequentFailedBuild,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual("Build Broken", rulesEngine.TrayNotificationEvents[0].Title);
            Assert.AreEqual("Build newly broken by User1 for Build Def 1", rulesEngine.TrayNotificationEvents[0].TipText);
            Assert.AreEqual("Build Broken", rulesEngine.TrayNotificationEvents[1].Title);
            Assert.AreEqual("Build still broken for Build Def 1", rulesEngine.TrayNotificationEvents[1].TipText);
            Assert.AreEqual(ToolTipIcon.Error, rulesEngine.TrayNotificationEvents[1].TipIcon);
        }

        [TestMethod]
        public void BuidFailsThenSucceedsWithGlobalSubsequentSuccessAlerts_NoAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void SuccessThenInProgressThenSuccessWithGlobalSuccessAlert_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual("Build Passing", rulesEngine.TrayNotificationEvents[0].Title);
            Assert.AreEqual("Build passed for Build Def 1", rulesEngine.TrayNotificationEvents[0].TipText);
            Assert.AreEqual(ToolTipIcon.Info, rulesEngine.TrayNotificationEvents[0].TipIcon);
        }

        [TestMethod]
        public void BuidInitiallyFailsThenSucceedsThenFailsThenSucceedsWithGlobalSubsequentSuccessAlert_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(1, rulesEngine.TrayNotificationEvents.Count);
            Assert.AreEqual("Build Passing", rulesEngine.TrayNotificationEvents[0].Title);
            Assert.AreEqual("Build passed for Build Def 1", rulesEngine.TrayNotificationEvents[0].TipText);
        }

        [TestMethod]
        public void BuildInitiallyFailsWithGlobalPlayLightsAlert_SetLights()
        {
            var rulesEngine = new RulesEngineWrapper();

            var ledPattern = new LedPattern { Id = 2, Name = "Sally" };
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                LedPattern = ledPattern,
                LightsDuration = 60,
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.SetAudioEvents.Count);
            Assert.AreEqual(1, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 60), rulesEngine.SetLightsEvents[0].TimeSpan);
            Assert.AreEqual(ledPattern, rulesEngine.SetLightsEvents[0].LedPattern);
        }

        [TestMethod]
        public void BuildInitiallyFailsWithGlobalPlayAudioAlert_SetAudio()
        {
            var rulesEngine = new RulesEngineWrapper();

            var audioPattern = new AudioPattern { Id = 2, Name = "Sally" };
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AudioPattern = audioPattern,
                AudioDuration = 60,
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.SetAudioEvents.Count);
            Assert.AreEqual(0, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 60), rulesEngine.SetAudioEvents[0].TimeSpan);
            Assert.AreEqual(audioPattern, rulesEngine.SetAudioEvents[0].AudioPattern);
        }

        [TestMethod]
        public void GlobalPlayAudioAlertButOnMute_NoAudio()
        {
            var rulesEngine = new RulesEngineWrapper();

            var audioPattern = new AudioPattern { Id = 2, Name = "Sally" };
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AudioPattern = audioPattern,
                AudioDuration = 60,
            });

            rulesEngine.Settings.Mute = true;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.SetAudioEvents.Count);
        }

        [TestMethod]
        public void BuildInitiallyFailsThenPassesWithGlobalPlayLightsUntilBuildFixedAlert_SetLightsOnThenOff()
        {
            var rulesEngine = new RulesEngineWrapper();

            var ledPattern = new LedPattern { Id = 2, Name = "Real Fast" };
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                LedPattern = ledPattern,
                LightsDuration = null,
                // until the build passes
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);

            Assert.AreEqual(1, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(null, rulesEngine.SetLightsEvents[0].TimeSpan);
            Assert.AreEqual(ledPattern, rulesEngine.SetLightsEvents[0].LedPattern);

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            Assert.AreEqual(2, rulesEngine.SetLightsEvents.Count);
            Assert.AreEqual(null, rulesEngine.SetLightsEvents[1].LedPattern);
        }

        [TestMethod]
        public void BuidFailsTwiceWithGlobalBuildFailAlert_TwoAlerts()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.TrayAlert
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void BuidFailsWithSomeoneElseBuildFailAlert_NoAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.ModalDialog,
                TriggerPerson = "someone else"
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.ModalDialogEvents.Count);
        }

        [TestMethod]
        public void BuidFailsWithMeBuildFailAlert_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.ModalDialog,
                TriggerPerson = RulesEngineWrapper.CURRENT_USER
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
        }

        [TestMethod]
        public void BuidFailsForSomeOtherBuild_NoAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.ModalDialog,
                BuildDefinitionId = "SomeOtherBuild"
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(0, rulesEngine.ModalDialogEvents.Count);
        }

        [TestMethod]
        public void BuidFailsForMyBuild_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.ModalDialog,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
        }

        [TestMethod]
        public void GlobalBuildDefTrayAlertConflictsWithLocalBuildDefModalAlert_ModalAlertOnly()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.TrayAlert,
            });
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.ModalDialog,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void InitialFailedBuildConflictsWithBuildFailedOrder1_InitialFailedBuildWins()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.TrayAlert,
            });
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.ModalDialog,
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void InitialFailedBuildConflictsWithBuildFailedOrder2_InitialFailedBuildWins()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.ModalDialog,
            });
            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.BuildFailed,
                AlertType = AlertType.TrayAlert,
            });

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.ModalDialogEvents.Count);
            Assert.AreEqual(0, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void NewBuildWithNewRequestedBy_PersonAdded()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.NoAlert,
            });

            Assert.AreEqual(0, rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People.Count);
            Assert.AreEqual(RulesEngineWrapper.CURRENT_USER,
                            rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People[0]);
        }

        [TestMethod]
        public void NewBuildWithExistingRequestedBy_PersonNotAdded()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.NoAlert,
            });

            Assert.AreEqual(0, rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People.Count);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People.Count);
            Assert.AreEqual("User1", rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People[0]);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);
            Assert.AreEqual(1, rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People.Count);
            Assert.AreEqual("User1", rulesEngine.Settings.CiEntryPointSettings[0].BuildDefinitionSettings[0].People[0]);
        }

        [TestMethod]
        public void NewBuildOnProject2_RequestByNotAddedToProject1()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.NoAlert,
            });

            BuildDefinitionSetting build1Setting = rulesEngine.GetBuildDefinitionSetting(RulesEngineWrapper.BUILD1_ID);
            Assert.AreEqual(0, build1Setting.People.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Broken,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2010, 1, 1)
            });
            Assert.AreEqual(0, build1Setting.People.Count);
        }

        [TestMethod]
        public void NewBuildWithEmptyRequestedBy_RequestByNotAdded()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.InitialFailedBuild,
                AlertType = AlertType.NoAlert,
            });

            BuildDefinitionSetting build1Setting = rulesEngine.GetBuildDefinitionSetting(RulesEngineWrapper.BUILD1_ID);
            Assert.AreEqual(0, build1Setting.People.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Broken,
                Name = RulesEngineWrapper.BUILD1_ID,
                RequestedBy = "",
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2010, 1, 1)
            });
            Assert.AreEqual(0, build1Setting.People.Count);
        }

        [TestMethod]
        public void BuildChangesNothingChanged_NoAdditionalInvokeRefreshStatus()
        {
            var rulesEngine = new RulesEngineWrapper();

            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2010, 1, 1)
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2010, 1, 1)
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
        }

        [TestMethod]
        public void BuildStartTimeChanged_RefreshStatus()
        {
            var rulesEngine = new RulesEngineWrapper();
            var firstStartTime = new DateTime(2011, 1, 1, 1, 1, 1);
            var secondStartTime = new DateTime(2022, 2, 2, 2, 2, 2);

            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = firstStartTime
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);

            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = secondStartTime
            });
            Assert.AreEqual(3, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual(BuildStatus.FormatAsDayMonthTime(secondStartTime), rulesEngine.RefreshStatusEvents.Last().BuildStatusDtos.First().StartTimeShort);
        }

        [TestMethod]
        public void InvokeStatusOnceForBuildAThenForBuildB_SecondRefreshStatusReturnsBuildAAndBuildB()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD1_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD1_ID,
                StartedTime = new DateTime(2011, 1, 1, 1, 1, 1)
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2011, 1, 1, 1, 1, 1)
            });
            Assert.AreEqual(3, rulesEngine.RefreshStatusEvents.Count);
            var lastRefreshStatusEvent = rulesEngine.RefreshStatusEvents.Last();
            Assert.AreEqual(2, lastRefreshStatusEvent.BuildStatusDtos.Count());
        }

        [TestMethod]
        public void BreakThenFixesBuild_UserHasStatsUpdated()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken); // it should ignore initial states
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);

            Assert.AreEqual(1, rulesEngine.Settings.People.Count);
            Assert.AreEqual(1, rulesEngine.Settings.People[0].TotalBuilds);
            Assert.AreEqual(1, rulesEngine.Settings.People[0].FailedBuilds);

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);

            Assert.AreEqual(1, rulesEngine.Settings.People[0].TotalBuilds);
            Assert.AreEqual(1, rulesEngine.Settings.People[0].FailedBuilds);

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);

            Assert.AreEqual(2, rulesEngine.Settings.People[0].TotalBuilds);
            Assert.AreEqual(1, rulesEngine.Settings.People[0].FailedBuilds);
        }

        [TestMethod]
        public void InProgressDoesNotWrite()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working); // do not write initial states
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);

            var lines = rulesEngine.SosDb.ReadAll(RulesEngineWrapper.BUILD1_ID);

            Assert.AreEqual(1, lines.Count);
            var buildDefinition = lines.First();
            Assert.AreEqual("User1", buildDefinition.RequestedBy);
            Assert.AreEqual(new DateTime(2010, 1, 1, 1, 1, 1), buildDefinition.StartedTime);
            Assert.AreEqual(new DateTime(2010, 1, 1, 1, 10, 10), buildDefinition.FinishedTime);
            Assert.AreEqual(null, buildDefinition.Comment);
            Assert.AreEqual(BuildStatusEnum.Broken, buildDefinition.BuildStatusEnum);
        }

        [TestMethod]
        public void InitialStatesDoNotWrite()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working); // do not write initial states
            var lines = rulesEngine.SosDb.ReadAll(RulesEngineWrapper.BUILD1_ID);
            Assert.AreEqual(0, lines.Count);
        }
    }
}
