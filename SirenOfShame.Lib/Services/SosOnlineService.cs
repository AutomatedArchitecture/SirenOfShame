using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SignalR.Client;
using SignalR.Client.Hubs;
using SirenOfShame.Lib.Dto;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Network;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace SirenOfShame.Lib.Services
{
    public class ApiResultBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }

    public class SosOnlineService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SosOnlineService));
        //public const string SOS_URL = "http://sirenofshame.com";
        public const string SOS_URL = "http://localhost.:3115";
        public event NewSosOnlineNotification OnNewSosOnlineNotification;
        public event SosOnlineStatusChange OnSosOnlineStatusChange;

        private void InvokeOnSosOnlineStatusChange(string status, Exception exception = null)
        {
            SosOnlineStatusChange handler = OnSosOnlineStatusChange;
            if (handler != null) handler(this, new SosOnlineStatusChangeArgs { TextStatus = status, Exception = exception });
        }

        private void InvokeOnOnNewSosOnlineNotification(dynamic data)
        {
            InvokeOnOnNewSosOnlineNotification(data.Message.Value,
                                               data.DisplayName.Value,
                                               data.ImageUrl.Value,
                                               data.EventTypeId.Value,
                                               data.Username.Value
                                               );
        }

        private void InvokeOnOnNewSosOnlineNotification(string message, string displayName, string imageUrl, long eventTypeId, string userName)
        {
            var args = new NewSosOnlineNotificationArgs
            {
                Message = message, 
                DisplayName = displayName,
                ImageUrl = imageUrl,
                EventTypeId = (int)eventTypeId,
                UserName = userName,
            };
            NewSosOnlineNotification handler = OnNewSosOnlineNotification;
            if (handler != null) handler(this, args);
        }

        public virtual void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string, ServerUnavailableException> onFail)
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

        public virtual void BuildStatusChanged(SirenOfShameSettings settings, IList<BuildStatus> changedBuildStatuses, List<OfflineUserDto> changedUsers)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.Add("ChangedBuildStatuses", JsonConvert.SerializeObject(changedBuildStatuses));
            webClientXml.Add("ChangedUsers", JsonConvert.SerializeObject(changedUsers));
            if (settings.SoftwareInstanceId.HasValue)
                webClientXml.Add("SoftwareInstanceId", settings.SoftwareInstanceId.Value.ToString(CultureInfo.InvariantCulture));
            const string url = SOS_URL + "/ApiV1/BuildStatusChangedV1";
            webClientXml.UploadValuesAndReturnStringAsync(url,
                                                       resultsStr =>
                                                       {
                                                           var result = JsonConvert.DeserializeObject<ApiResultBase>(resultsStr);
                                                           if (!result.Success)
                                                           {
                                                               string errorMessage = result.Message;
                                                               _log.Error("Error publishing to: " + url + " error: " + errorMessage);
                                                           }
                                                       }, ex => _log.Error("Error publishing to: " + url, ex), settings.GetSosOnlineProxy());
        }

        public virtual void Synchronize(SirenOfShameSettings settings, string exportedBuilds, string exportedAchievements, Action<DateTime> onSuccess, Action<string, ServerUnavailableException> onFail)
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

        protected virtual SosWebClient GetWebClient()
        {
            return new SosWebClient();
        }

        public virtual void TryToGetAndSendNewSosOnlineAlerts(SirenOfShameSettings settings, DateTime now, Action<NewAlertEventArgs> invokeNewAlert)
        {
            if (!settings.GetSosOnlineContent()) return;

            bool weHaveAlreadyCheckedForAlertsToday = settings.LastCheckedForAlert != null && (now - settings.LastCheckedForAlert.Value).TotalHours < 24;
            if (weHaveAlreadyCheckedForAlertsToday) return;

            settings.LastCheckedForAlert = DateTime.Now;
            settings.Save();
            var webClient = GetWebClient();
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

        private HubConnection _connection;
        private IHubProxy _proxy;

        public virtual void StartRealtimeConnection(SirenOfShameSettings settings)
        {
            try
            {
                if (!settings.GetSosOnlineContent())
                {
                    InvokeOnSosOnlineStatusChange("Disabled");
                    return;
                }
                InvokeOnSosOnlineStatusChange("Connecting");
                _connection = new HubConnection(SOS_URL);
                _proxy = _connection.CreateProxy("SosHub");
                _proxy.On("addAppNotificationV1", InvokeOnOnNewSosOnlineNotification);
                _connection.Error += ConnectionOnError;
                _connection.StateChanged += ConnectionOnStateChanged;
                _connection.Closed += ConnectionOnClosed;
                Task result = _connection.Start();
                result.ContinueWith(ConnectionOnError, TaskContinuationOptions.OnlyOnFaulted);
            } 
            catch (Exception ex)
            {
                _log.Error("Unable to start realtime connection to SoS Online", ex);
            }
        }

        private void ConnectionOnClosed()
        {
            _log.Debug("Sos Online: closed");
            InvokeOnSosOnlineStatusChange("Closed");
        }

        private void ConnectionOnStateChanged(StateChange stateChange)
        {
            _log.Debug("Sos Online: " + stateChange.NewState);
            if (stateChange.NewState == ConnectionState.Connected)
                InvokeOnSosOnlineStatusChange("Connected");
            if (stateChange.NewState == ConnectionState.Disconnected)
                InvokeOnSosOnlineStatusChange("Disconnected");
            if (stateChange.NewState == ConnectionState.Reconnecting)
                InvokeOnSosOnlineStatusChange("Reconnecting");
            if (stateChange.NewState == ConnectionState.Connecting)
                InvokeOnSosOnlineStatusChange("Connecting");
        }

        private void ConnectionOnError(Task obj)
        {
            ConnectionOnError(obj.Exception);
        }

        private void ConnectionOnError(Exception ex)
        {
            InvokeOnSosOnlineStatusChange("Error", ex);
            _log.Error("Error connecting to SoS Online via SignalR", ex);
        }

        private readonly Dictionary<string, int> _cachedAvatarIds = new Dictionary<string, int>();

        public virtual SosOnlinePerson CreateSosOnlinePersonFromSosOnlineNotification(NewSosOnlineNotificationArgs args, ImageList avatarImageList)
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
                if (!_cachedAvatarIds.TryGetValue(args.ImageUrl, out avatarId))
                {
                    avatarId = GetGravatarFromWebAndAddToImageList(args, avatarImageList);
                    _cachedAvatarIds[args.ImageUrl] = avatarId;
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

        public virtual void SendMessage(SirenOfShameSettings settings, string message)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.Add("Message", message);
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/ApiV1/AddMessage", doc =>
            {
                string success = doc.Descendants("Success").First().Value;
                if (success == "true")
                {
                    _log.Debug("Message successfully added");
                }
                else
                {
                    string errorMessage = doc.Descendants("ErrorMessage").First().Value;
                    _log.Error("Failed to add message: " + errorMessage);
                }
            }, OnConnectionFail, settings.GetSosOnlineProxy());
        }

        private void OnConnectionFail(ServerUnavailableException obj)
        {
            _log.Error("Failed to connect to SoS Online.", obj);
        }
    }

    public delegate void SosOnlineStatusChange(object sender, SosOnlineStatusChangeArgs args);

    public class SosOnlineStatusChangeArgs
    {
        public string TextStatus { get; set; }
        public Exception Exception { get; set; }
    }
}
