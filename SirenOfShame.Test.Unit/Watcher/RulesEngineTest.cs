using System;
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
            rulesEngine.InvokeStatusChecked(new BuildStatus[] {});
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
            rulesEngine.InvokeStatusChecked(new BuildStatus[] {});
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
            rulesEngine.InvokeStatusChecked(new BuildStatus[] {});
            rulesEngine.InvokeStatusChecked(new BuildStatus[] {});
            Assert.AreEqual(2, rulesEngine.TrayNotificationEvents.Count);
        }

        [TestMethod]
        public void InitialStatusChecked_RefreshStatus()
        {
            var rulesEngine = new RulesEngineWrapper();

            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents.Count);

            rulesEngine.InvokeStatusChecked(new[]
            {
                new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.Working,
                    Name = "Build Def 1",
                    RequestedBy = "User1",
                    BuildDefinitionId = "Build Def 1",
                    StartedTime = new DateTime(2010, 1, 2, 1, 1, 1),
                    FinishedTime = new DateTime(2010, 1, 2, 1, 2, 2)
                },
            });

            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual(1, rulesEngine.RefreshStatusEvents[1].BuildStatusListViewItems.Count());
            BuildStatusListViewItem buildStatus = rulesEngine.RefreshStatusEvents[1].BuildStatusListViewItems.First();
            Assert.AreEqual((int) BallsEnum.Green, buildStatus.ImageIndex);
            Assert.AreEqual("Build Def 1", buildStatus.Name);
            Assert.AreEqual("User1", buildStatus.RequestedBy);
            Assert.AreEqual("Build Def 1", buildStatus.Id);
            Assert.AreEqual("1/2 1:01 AM", buildStatus.StartTime);
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
        public void BuidInitiallySucceedsWithGlobalSubsequentSuccessAlert_OneAlert()
        {
            var rulesEngine = new RulesEngineWrapper();

            rulesEngine.Rules.Add(new Rule
            {
                TriggerType = TriggerType.SuccessfulBuild,
                AlertType = AlertType.TrayAlert
            });

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

            var ledPattern = new LedPattern {Id = 2, Name = "Sally"};
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

            var audioPattern = new AudioPattern {Id = 2, Name = "Sally"};
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
        public void BuildInitiallyFailsThenPassesWithGlobalPlayLightsUntilBuildFixedAlert_SetLightsOnThenOff()
        {
            var rulesEngine = new RulesEngineWrapper();

            var ledPattern = new LedPattern {Id = 2, Name = "Real Fast"};
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

            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2011, 1, 1, 1, 1, 1)
            });
            Assert.AreEqual(2, rulesEngine.RefreshStatusEvents.Count);
            rulesEngine.InvokeStatusChecked(new BuildStatus
            {
                BuildStatusEnum = BuildStatusEnum.Working,
                Name = RulesEngineWrapper.BUILD2_ID,
                RequestedBy = RulesEngineWrapper.CURRENT_USER,
                BuildDefinitionId = RulesEngineWrapper.BUILD2_ID,
                StartedTime = new DateTime(2022, 2, 2, 2, 2, 2)
            });
            Assert.AreEqual(3, rulesEngine.RefreshStatusEvents.Count);
            Assert.AreEqual("2/2 2:02 AM",
                            rulesEngine.RefreshStatusEvents.Last().BuildStatusListViewItems.First().StartTime);
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
            Assert.AreEqual(2, lastRefreshStatusEvent.BuildStatusListViewItems.Count());
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
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Broken);

            Assert.AreEqual(1, rulesEngine.SosDb.Files.Keys.Count);
            var keyValuePair = rulesEngine.SosDb.Files.First();
            Assert.AreEqual(true, keyValuePair.Key.EndsWith("Build Def 1.txt"));
            var contents = keyValuePair.Value;
            Assert.AreEqual(3, contents.Split('\n').Length);
            Assert.AreEqual(@"633979008000000000,,1,User1
633979008000000000,,2,User1
", contents);
        }
        
        [TestMethod]
        public void InitialStatesDoNotWrite()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working); // do not write initial states
            Assert.AreEqual(0, rulesEngine.SosDb.Files.Keys.Count);
        }
}
}
