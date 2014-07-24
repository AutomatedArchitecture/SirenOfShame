using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using SirenOfShame.Extruder.Models;

namespace SirenOfShame.Extruder.Services
{
    public class SosOnlineService
    {
        private const string SOS_URL = "http://localhost:3115";
        private readonly ILog _log = MyLogManager.GetLog(typeof (SosOnlineService));
        private HubConnection _connection;
        private IHubProxy _proxy;
        public Action<StateChange> StatusChanged { get; set; }

        public async Task<ApiResultBase> ConnectExtruder(ConnectExtruderModel connectExtruderModel)
        {
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(connectExtruderModel);
            const string url = SOS_URL + "/ApiV1/ConnectExtruder";
            var resultStr = await webClient.UploadStringTaskAsync(url, "POST", data);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResultBase>(resultStr);
            return result;
        }

        public async Task StartRealtimeConnection(ConnectExtruderModel connectExtruderModel)
        {
            try
            {
                _connection = new HubConnection(SOS_URL);
                _proxy = _connection.CreateHubProxy("SosHub");
                _proxy.On("playSiren", OnPlaySirenEventReceived);
                _connection.Error += ConnectionOnError;
                _connection.StateChanged += ConnectionOnStateChanged;
                _connection.Closed += ConnectionOnClosed;
                await _connection.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Unable to start realtime connection to SoS Online", ex);
            }
        }

        private void ConnectionOnClosed()
        {
            MessageBox.Show("On connection closed");
        }

        private void ConnectionOnStateChanged(StateChange obj)
        {
            _log.Debug("State changed to " + obj.NewState);
            if (StatusChanged != null) StatusChanged(obj);
        }

        private void ConnectionOnError(Exception ex)
        {
            _log.Error("SignalR Connection Error", ex);
        }

        private void OnPlaySirenEventReceived()
        {
            MessageBox.Show("Received an alert!");
        }
    }
}
