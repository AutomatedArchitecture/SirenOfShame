using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using SignalR.Client.Hubs;
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
        public const string SOS_URL = "http://localhost:3115";
        public event NewSosOnlineNotification OnNewSosOnlineNotification;

        public void InvokeOnOnNewSosOnlineNotification(NewSosOnlineNotificationArgs args)
        {
            NewSosOnlineNotification handler = OnNewSosOnlineNotification;
            if (handler != null) handler(this, args);
        }
        
        public void InvokeOnOnNewSosOnlineNotification(string message, string displayName, string imageUrl)
        {
            InvokeOnOnNewSosOnlineNotification(new NewSosOnlineNotificationArgs
            {
                Message = message, 
                DisplayName = displayName,
                ImageUrl = imageUrl,
            });
        }

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
            }, OnConnectionFail(onFail), settings.GetSosOnlineProxy());
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
            }, OnConnectionFail(onFail), settings.GetSosOnlineProxy());
        }

        private static Action<ServerUnavailableException> OnConnectionFail(Action<string, ServerUnavailableException> onFail)
        {
            return ex => onFail("Failed to connect to SoS online", ex);
        }

        public void TryToGetAndSendNewSosOnlineAlerts(SirenOfShameSettings settings, DateTime now, Action<NewAlertEventArgs> invokeNewAlert, SosWebClient webClient)
        {
            if (!settings.GetSosOnlineContent()) return;

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
            string url = string.Format(SOS_URL + "/GetAlert?SirenEverConnected={0}&SoftwareInstanceId={1}&ServerType={2}&Version={3}",
                settings.SirenEverConnected,
                settings.SoftwareInstanceId,
                string.Join(",", settings.CiEntryPointSettings.Select(cip => cip.Name)),
                Application.ProductVersion
                );
            webClient.DownloadStringAsync(new Uri(url));
        }

        public void StartRealtimeConnection(SirenOfShameSettings settings)
        {
            try
            {
                if (!settings.GetSosOnlineContent()) return;
                var connection = new HubConnection(SOS_URL);
                var proxy = connection.CreateProxy("SosHub");
                proxy.On("addAppNotificationV1",
                         data => InvokeOnOnNewSosOnlineNotification(data.Message.Value, data.DisplayName.Value, data.ImageUrl.Value));
                connection.Error += ex => _log.Error("Error connecting to SoS Online via SignalR", ex);
                Task result = connection.Start();
                result.ContinueWith(t => _log.Error("Error connecting to SoS Online via SignalR", t.Exception),
                                    TaskContinuationOptions.OnlyOnFaulted);
            } 
            catch (Exception ex)
            {
                _log.Error("Unable to start realtime connection to SoS Online", ex);
            }
        }

        private Dictionary<string, int> cachedAvatarIds = new Dictionary<string, int>();

        public SosOnlinePerson CreateSosOnlinePersonFromSosOnlineNotification(NewSosOnlineNotificationArgs args, ImageList avatarImageList)
        {
            var avatarId = GetAvatarId(args, avatarImageList);
            return new SosOnlinePerson
            {
                AvatarId = avatarId,
                DisplayName = args.DisplayName
            };
        }

        private int GetAvatarId(NewSosOnlineNotificationArgs args, ImageList avatarImageList)
        {
            int avatarId;
            try
            {
                if (!cachedAvatarIds.TryGetValue(args.ImageUrl, out avatarId))
                {
                    avatarId = GetGravatarFromWebAndAddToImageList(args, avatarImageList);
                    cachedAvatarIds[args.ImageUrl] = avatarId;
                }
            } catch (Exception ex)
            {
                _log.Error("Error retrieving gravatar for " + args.DisplayName, ex);
                avatarId = SirenOfShameSettings.GenericSosOnlineAvatarId;
            }
            return avatarId;
        }

        private static int GetGravatarFromWebAndAddToImageList(NewSosOnlineNotificationArgs args, ImageList avatarImageList)
        {
            int avatarId;
            var webClient = new WebClient();
            byte[] imageRaw = webClient.DownloadData(args.ImageUrl);
            using (MemoryStream gravatarMs = new MemoryStream(imageRaw))
            {
                Image gravatarImage = Image.FromStream(gravatarMs);
                avatarId = avatarImageList.Images.Add(gravatarImage, Color.Transparent);
            }
            return avatarId;
        }
    }
}
