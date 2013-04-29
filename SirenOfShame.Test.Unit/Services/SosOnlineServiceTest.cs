using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Watcher;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Services
{
    [TestClass]
    public class SosOnlineServiceTest
    {
        [TestMethod]
        public void BuildStatusChanged_InProgressBuilds_DoSync()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.SosOnlineAlwaysSync = true;
            rulesEngine.Settings.SosOnlineWhatToSync = WhatToSyncEnum.BuildStatuses;
            rulesEngine.Settings.SosOnlineUsername = "mockUsername";
            var mock = new Mock<SosOnlineService>();
            rulesEngine.SosOnlineService = mock.Object;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            mock.Verify(i => i.BuildStatusChanged(It.IsAny<SirenOfShameSettings>(), It.IsAny<IList<BuildStatus>>()), Times.Exactly(2));
        }

        [TestMethod]
        public void BuildStatusChanged_MyPointAndAchievementsOnly_DoesNotSync()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.SosOnlineAlwaysSync = true;
            rulesEngine.Settings.SosOnlineWhatToSync = WhatToSyncEnum.MyPointAndAchievementsOnly;
            rulesEngine.Settings.SosOnlineUsername = "mockUsername";
            var mock = new Mock<SosOnlineService>();
            rulesEngine.SosOnlineService = mock.Object;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            mock.Verify(i => i.BuildStatusChanged(It.IsAny<SirenOfShameSettings>(), It.IsAny<IList<BuildStatus>>()), Times.Never());
        }

        [TestMethod]
        public void BuildStatusChanged_SyncBuildStatuses_DoesSync()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.SosOnlineAlwaysSync = true;
            rulesEngine.Settings.SosOnlineWhatToSync = WhatToSyncEnum.BuildStatuses;
            rulesEngine.Settings.SosOnlineUsername = "mockUsername";
            var mock = new Mock<SosOnlineService>();
            rulesEngine.SosOnlineService = mock.Object;
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            mock.Verify(i => i.BuildStatusChanged(It.IsAny<SirenOfShameSettings>(), It.IsAny<IList<BuildStatus>>()), Times.Once());
        }

        [TestMethod]
        public void Synchronize_ICheckInWithAlwaysSyncSetting_SosOnlineServiceSynchronize()
        {
            var rulesEngine = new RulesEngineWrapper();
            rulesEngine.Settings.SosOnlineAlwaysSync = true;
            rulesEngine.Settings.SosOnlineUsername = "mockUsername";
            rulesEngine.Settings.MyRawName = RulesEngineWrapper.CURRENT_USER;

            var sosOnlineService = new Mock<SosOnlineService>();
            sosOnlineService.Setup(i => i.Synchronize(It.IsAny<SirenOfShameSettings>(), "633979044610000000,633979050100000000,1", null, It.IsAny<Action<DateTime>>(), It.IsAny<Action<String, ServerUnavailableException>>()))
                .Verifiable();
            rulesEngine.SosOnlineService = sosOnlineService.Object;

            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.InProgress);
            rulesEngine.InvokeStatusChecked(BuildStatusEnum.Working);

            sosOnlineService.Verify();
        }

        [TestMethod]
        public void TryToGetAndSendNewSosOnlineAlerts_HaveNeverCheckedForAlerts_SendAlertToUi()
        {
            var sosOnlineService = new SosOnlineServiceFake();
            var settings = new SirenOfShameSettingsFake();
            settings.LastCheckedForAlert = null;
            NewAlertEventArgs newAlertEventArgs = null;
            sosOnlineService.TryToGetAndSendNewSosOnlineAlerts(settings, new DateTime(2010, 1, 1), alert =>
            {
                newAlertEventArgs = alert;
            });

            sosOnlineService.FakeWebClient.InvokeDownloadStringCompleted(@"56
http://www.google.com
Hello World
633979872000000000");

            Assert.IsNotNull(newAlertEventArgs, "Expected WebClient.DownloadStringAsync to be called, but it never was");
            Assert.AreEqual("Hello World", newAlertEventArgs.Message);
            Assert.AreEqual("http://www.google.com", newAlertEventArgs.Url);
            Assert.AreEqual(new DateTime(2010, 1, 2), newAlertEventArgs.AlertDate);
        }

        [TestMethod]
        public void TryToGetAndSendNewSosOnlineAlerts_HaveNeverCheckedForAlertsButSettingsSayNeverDownload_DoNotSendAlertToUi()
        {
            var sosOnlineService = new SosOnlineServiceFake();
            var settings = new SirenOfShameSettingsFake();
            settings.UpdateLocation = UpdateLocation.Never;
            settings.LastCheckedForAlert = null;
            NewAlertEventArgs newAlertEventArgs = null;
            sosOnlineService.TryToGetAndSendNewSosOnlineAlerts(settings, new DateTime(2010, 1, 1), alert =>
            {
                newAlertEventArgs = alert;
            });

            sosOnlineService.FakeWebClient.InvokeDownloadStringCompleted(@"56
http://www.google.com
Hello World
633979872000000000");
            Assert.IsNull(newAlertEventArgs);
        }

        [TestMethod]
        public void TryToGetAndSendNewSosOnlineAlerts_LastCheckedForNewAlertsOver24HoursAgoButThisIsTheSameAlert_NoAlert()
        {
            var sosOnlineService = new SosOnlineServiceFake();
            var settings = new SirenOfShameSettingsFake();
            
            
            var now = new DateTime(2010, 2, 2, 2, 2, 2);
            settings.LastCheckedForAlert = new DateTime(2010, 2, 1, 2, 2, 1);
            settings.AlertClosed = new DateTime(2010, 1, 2);

            NewAlertEventArgs newAlertEventArgs = null;
            sosOnlineService.TryToGetAndSendNewSosOnlineAlerts(settings, now, alert =>
            {
                newAlertEventArgs = alert;
            });

            sosOnlineService.FakeWebClient.InvokeDownloadStringCompleted(@"56
http://www.google.com
Hello World
633979872000000000");
            Assert.IsNull(newAlertEventArgs);
        }

        [TestMethod]
        public void TryToGetAndSendNewSosOnlineAlerts_LastCheckedForNewAlerts24HoursAndOneOneSecondAgoAndThisIsANewAlert_SendAlertToUi()
        {
            var sosOnlineService = new SosOnlineServiceFake();
            var settings = new SirenOfShameSettingsFake();
            
            var now = new DateTime(2010, 2, 2, 2, 2, 2);
            settings.LastCheckedForAlert = new DateTime(2010, 2, 1, 2, 2, 1);
            settings.AlertClosed = new DateTime(2010, 1, 1);

            NewAlertEventArgs newAlertEventArgs = null;
            sosOnlineService.TryToGetAndSendNewSosOnlineAlerts(settings, now, alert =>
            {
                newAlertEventArgs = alert;
            });

            sosOnlineService.FakeWebClient.InvokeDownloadStringCompleted(@"56
http://www.google.com
Hello World
633979872000000000");
            Assert.IsNotNull(newAlertEventArgs);
        }

        [TestMethod]
        public void TryToGetAndSendNewSosOnlineAlerts_LastCheckedForNewAlertsOneSecondAgo_DoNotSendAlertToUi()
        {
            var sosOnlineService = new SosOnlineServiceFake();
            var settings = new SirenOfShameSettingsFake();
            var now = new DateTime(2010, 2, 2, 2, 2, 1);
            settings.LastCheckedForAlert = new DateTime(2010, 2, 2, 2, 2, 2);

            NewAlertEventArgs newAlertEventArgs = null;
            sosOnlineService.TryToGetAndSendNewSosOnlineAlerts(settings, now, alert =>
            {
                newAlertEventArgs = alert;
            });

            sosOnlineService.FakeWebClient.InvokeDownloadStringCompleted(@"56
http://www.google.com
Hello World
633979872000000000");
            
            Assert.IsNull(newAlertEventArgs);
        }
    }
}
