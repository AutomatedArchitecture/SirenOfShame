using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using SirenOfShame.Lib.Dto;
using SirenOfShame.Lib.Network;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Services
{
    public class DesktopAppConnectionResult : ApiResultBase
    {
        public bool ChatEnabled { get; set; }
    }

    public partial class SosOnlineService
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SosOnlineService));
        public static readonly string SOS_URL = "http://sirenofshame.com";
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
                                               data.Username.Value
                                               );
        }

        private void InvokeOnOnNewSosOnlineNotification(string message, string displayName, string imageUrl, string userName)
        {
            var args = new NewSosOnlineNotificationArgs
            {
                Message = message, 
                DisplayName = displayName,
                ImageUrl = imageUrl,
                UserName = userName,
            };
            NewSosOnlineNotification handler = OnNewSosOnlineNotification;
            if (handler != null) handler(this, args);
        }

        public virtual void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string, Exception> onFail)
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

        public virtual void BuildStatusChanged(SirenOfShameSettings settings, IList<BuildStatus> changedBuildStatuses, List<InstanceUserDto> changedUsers)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.Add("ChangedBuildStatuses", JsonConvert.SerializeObject(changedBuildStatuses));
            webClientXml.Add("ChangedUsers", JsonConvert.SerializeObject(changedUsers));
            if (settings.SoftwareInstanceId.HasValue)
                webClientXml.Add("SoftwareInstanceId", settings.SoftwareInstanceId.Value.ToString(CultureInfo.InvariantCulture));
            string url = SOS_URL + "/ApiV1/BuildStatusChangedV1";
            webClientXml.UploadValuesAndReturnStringAsync(url, ReadResult, ex => _log.Error("Error publishing to: " + url, ex), settings.GetSosOnlineProxy());
        }

        private static void ReadResult(string resultsStr)
        {
            ApiResultBase result;
            try
            {
                result = JsonConvert.DeserializeObject<ApiResultBase>(resultsStr);
            }
            catch (JsonReaderException ex)
            {
                _log.Error("Unable to parse result from build status changed: " + resultsStr, ex);
                return;
            }
            if (!result.Success)
            {
                string errorMessage = result.Message;
                _log.Error("Error publishing build status changed : " + errorMessage);
            }
        }

        public virtual void Synchronize(SirenOfShameSettings settings, string exportedBuilds, string exportedAchievements, Action<DateTime> onSuccess, Action<string, Exception> onFail)
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
            }, ex => onFail("Failed to connect to SoS Online", ex), settings.GetSosOnlineProxy());
        }

        private static Action<Exception> OnConnectionFail(Action<string, Exception> onFail)
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

        public virtual async Task<DesktopAppConnectionResult> StartRealtimeConnection(SirenOfShameSettings settings)
        {
            try
            {
                if (!settings.GetSosOnlineContent())
                {
                    InvokeOnSosOnlineStatusChange("Disabled");
                    return new DesktopAppConnectionResult { Success = false };
                }
                InvokeOnSosOnlineStatusChange("Connecting");
                _connection = new HubConnection(SOS_URL);
                _proxy = _connection.CreateHubProxy("SosHub");
                _proxy.On("addChatMessageToDesktopClients", InvokeOnOnNewSosOnlineNotification);
                _connection.Error += ConnectionOnError;
                _connection.StateChanged += ConnectionOnStateChanged;
                _connection.Closed += ConnectionOnClosed;
                await _connection.Start();
                var credentialApiModel = new CredentialApiModel
                {
                    UserName = settings.SosOnlineUsername,
                    Password = settings.SosOnlinePassword,
                };
                var result = await _proxy.Invoke<DesktopAppConnectionResult>("connectDesktopApp", credentialApiModel);
                if (!result.Success)
                {
                    _connection.Stop();
                }
                return result;
            } 
            catch (Exception ex)
            {
                _log.Error("Unable to start realtime connection to SoS Online", ex);
            }
            return new DesktopAppConnectionResult { Success = false };
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

        private void ConnectionOnError(Exception ex)
        {
            InvokeOnSosOnlineStatusChange("Error", ex);
            _log.Error("Error connecting to SoS Online via SignalR", ex);
        }

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
            var gravatarService = new GravatarService();
            int avatarId;
            try
            {
                avatarId = gravatarService.DownloadImageFromWebAndAddToImageList(args.ImageUrl, avatarImageList);
            } catch (Exception ex)
            {
                _log.Error("Error retrieving gravatar for " + args.DisplayName, ex);
                avatarId = SirenOfShameSettings.GenericSosOnlineAvatarId;
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

        private void OnConnectionFail(Exception obj)
        {
            _log.Error("Failed to connect to SoS Online.", obj);
        }
    }

    public delegate void SosOnlineStatusChange(object sender, SosOnlineStatusChangeArgs args);
}
