using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Network;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Services
{
    public class SosOnlineService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SosOnlineService));
        public const string SOS_URL = "http://sirenofshame.com";

        public void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string, ServerUnavailableException> onFail)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/ApiV1/VerifyCredentials", doc =>
            {
                string success = doc.Descendants("Success").First().Value;
                if (success == "true")
                {
                    onSuccess();
                } 
                else
                {
                    string errorMessage = doc.Descendants("ErrorMessage").First().Value;
                    onFail(errorMessage, null);
                }
            }, OnConnectionFail(onFail));
        }

        private static void AddSosOnlineCredentials(SirenOfShameSettings settings, WebClientXml webClientXml)
        {
            webClientXml.Add("UserName", settings.SosOnlineUsername);
            // send the encrypted version of the password since we're too cheap to support SSL at present
            webClientXml.Add("Password", settings.SosOnlinePassword);
        }

        public void Synchronize(SirenOfShameSettings settings, string exportedBuilds, string exportedAchievements, Action<DateTime> onSuccess, Action<string, ServerUnavailableException> onFail)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.Add("Builds", exportedBuilds);
            webClientXml.Add("Achievements", exportedAchievements);
            if (settings.SoftwareInstanceId.HasValue)
                webClientXml.Add("SoftwareInstanceId", settings.SoftwareInstanceId.Value.ToString(CultureInfo.InvariantCulture));
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/ApiV1/Synchronize", doc =>
            {
                string success = doc.Descendants("Success").First().Value;
                if (success == "true")
                {
                    string newHighWaterMarkStr = doc.Descendants("NewHighWaterMark").First().Value;
                    DateTime newHighWaterMark = new DateTime(long.Parse(newHighWaterMarkStr));
                    onSuccess(newHighWaterMark);
                }
                else
                {
                    string errorMessage = doc.Descendants("ErrorMessage").First().Value;
                    onFail(errorMessage, null);
                }
            }, OnConnectionFail(onFail));
        }

        private static Action<ServerUnavailableException> OnConnectionFail(Action<string, ServerUnavailableException> onFail)
        {
            return ex => onFail("Failed to connect to SoS online", ex);
        }

        public void TryToGetAndSendNewSosOnlineAlerts(SirenOfShameSettings settings, DateTime now, Action<NewAlertEventArgs> invokeNewAlert, SosWebClient webClient)
        {
            // if someone doesn't want to check for the lastest software, they probably are on a private network and don't want to check for alerts either
            if (settings.UpdateLocation != UpdateLocation.Auto) return;

            bool weHaveAlreadyCheckedForAlertsToday = settings.LastCheckedForAlert != null && (now - settings.LastCheckedForAlert.Value).TotalHours < 24;
            if (weHaveAlreadyCheckedForAlertsToday) return;

            settings.LastCheckedForAlert = DateTime.Now;
            settings.Save();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                try
                {
                    if (e.Error != null)
                    {
                        _log.Error("Error retrieving alert", e.Error);
                        return;
                    }
                    NewAlertEventArgs args = new NewAlertEventArgs();
                    var successParsing = args.Instantiate(e.Result);
                    if (successParsing)
                    {
                        if (settings.SoftwareInstanceId == null)
                        {
                            settings.SoftwareInstanceId = args.SoftwareInstanceId;
                            settings.Save();
                        }
                        if (settings.AlertClosed == null || args.AlertDate > settings.AlertClosed)
                        {
                            invokeNewAlert(args);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("Error retrieving alert", ex);
                }
            };
            string url = string.Format("http://sirenofshame.com/GetAlert?SirenEverConnected={0}&SoftwareInstanceId={1}&ServerType={2}&Version={3}",
                settings.SirenEverConnected,
                settings.SoftwareInstanceId,
                string.Join(",", settings.CiEntryPointSettings.Select(cip => cip.Name)),
                Application.ProductVersion
                );
            webClient.DownloadStringAsync(new Uri(url));
        }
    }
}
