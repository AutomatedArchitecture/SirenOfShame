using System;
using System.Threading.Tasks;
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
        public Action<int?, TimeSpan, int?, TimeSpan> PlaySiren { get; set; }

        public async Task<ApiResultBase> ConnectExtruder(ConnectExtruderModel connectExtruderModel)
        {
            return await StartRealtimeConnection(connectExtruderModel);
        }

        public async Task<ApiResultBase> StartRealtimeConnection(ConnectExtruderModel connectExtruderModel)
        {
            try
            {
                _connection = new HubConnection(SOS_URL);
                _proxy = _connection.CreateHubProxy("SosHub");

                _proxy.On<int?, TimeSpan, int?, TimeSpan>("playSiren", OnPlaySirenEventReceived);
                _connection.Error += ConnectionOnError;
                _connection.StateChanged += ConnectionOnStateChanged;
                _connection.Closed += ConnectionOnClosed;
                await _connection.Start();
                var result = await _proxy.Invoke<ApiResultBase>("connectExtruder", connectExtruderModel);
                return result;
            }
            catch (Exception ex)
            {
                _log.Error("Unable to start realtime connection to SoS Online", ex);
                return new ApiResultBase {Success = false, ErrorMessage = ex.Message};
            }
        }

        public void Disconnect()
        {
            if (_connection != null) _connection.Stop();
        }

        private void ConnectionOnClosed()
        {
            _log.Debug("Connection closed");
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

        private void OnPlaySirenEventReceived(int? ledPatternIndex, TimeSpan ledDuration, int? audioPatternIndex, TimeSpan audioDuration)
        {
            if (PlaySiren != null)
            {
                PlaySiren(ledPatternIndex, ledDuration, audioPatternIndex, audioDuration);
            }
        }
    }
}
