using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Watcher;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Services
{
    [TestClass]
    public class SosOnlineServiceTest
    {
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
