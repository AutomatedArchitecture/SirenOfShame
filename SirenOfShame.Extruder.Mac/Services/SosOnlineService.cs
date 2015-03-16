using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SirenOfShame.Extruder.Mac
{
	public delegate void TrayNotifyEvent(object sender, TrayNotifyEventArgs args);
	public delegate void SetTrayIconEvent(object sender, SetTrayIconEventArgs args);

	public class SosOnlineService
	{
		public static string SOS_URL = "http://sirenofshame.com";
		private HubConnection _connection;
		private IHubProxy _proxy;
		public Action<StateChange> StatusChanged { get; set; }
		public Action<int?, TimeSpan?> SetLights { get; set; }
		public Action<int?, TimeSpan?> SetAudio { get; set; }
		public Action<ModalDialogEventArgs> ModalDialog { get; set; }
		public event TrayNotifyEvent TrayNotify;

		public async Task<ApiResultBase> ConnectExtruder(ConnectExtruderModel connectExtruderModel)
		{
			return await StartRealtimeConnection(connectExtruderModel);
		}

		private async Task<ApiResultBase> StartRealtimeConnection(ConnectExtruderModel connectExtruderModel) {
			_connection = new HubConnection(SOS_URL);
			_proxy = _connection.CreateHubProxy("SosHub");

			_proxy.On<int?, TimeSpan?>("setLights", OnSetLightsEventReceived);
			_proxy.On<int?, TimeSpan?>("setAudio", OnSetAudioEventReceived);
			_proxy.On<ModalDialogEventArgs>("modalDialog", OnModalDialogEventReceived);
			_proxy.On("forceDisconnect", OnForceDisconnect);
			_proxy.On<TrayIcon>("setTrayIcon", OnSetTrayIcon);
			_proxy.On<TrayNotifyEventArgs>("trayNotify", OnTrayNotify);
			_connection.Error += ConnectionOnError;
			_connection.StateChanged += ConnectionOnStateChanged;
			_connection.Closed += ConnectionOnClosed;
			await _connection.Start();
			var result = await _proxy.Invoke<ApiResultBase>("connectExtruder", connectExtruderModel);
			if (!result.Success)
			{
				_connection.Stop();
			}
			return result;
		}

		private void OnTrayNotify(TrayNotifyEventArgs args)
		{
			var handler = TrayNotify;
			if (handler != null) handler(this, args);
		}

		public event SetTrayIconEvent SetTrayIcon;

		private void OnSetTrayIcon(TrayIcon trayIcon)
		{
			var handler = SetTrayIcon;
			if (handler != null) handler(this, new SetTrayIconEventArgs { TrayIcon = trayIcon });
		}

		private void OnForceDisconnect()
		{
			//_log.Info("Force disconnected from server");
			Disconnect();
		}

		public void Disconnect()
		{
			if (_connection != null) _connection.Stop();
		}

		private void ConnectionOnClosed()
		{
			//_log.Debug("Connection closed");
		}

		private void ConnectionOnStateChanged(StateChange obj)
		{
			//_log.Debug("State changed to " + obj.NewState);
			if (StatusChanged != null) StatusChanged(obj);
		}

		private void ConnectionOnError(Exception ex)
		{
			//_log.Error("SignalR Connection Error", ex);
		}

		private void OnSetAudioEventReceived(int? audioPatternIndex, TimeSpan? audioDuration)
		{
			if (SetAudio != null)
			{
				SetAudio(audioPatternIndex, audioDuration);
			}
		}

		private void OnSetLightsEventReceived(int? ledPatternIndex, TimeSpan? ledDuration)
		{
			if (SetLights != null)
			{
				SetLights(ledPatternIndex, ledDuration);
			}
		}

		private void OnModalDialogEventReceived(ModalDialogEventArgs args)
		{
			if (ModalDialog != null)
			{
				ModalDialog(args);
			}
		}
	}
}

