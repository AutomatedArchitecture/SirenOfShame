using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Device
{
    [Export(typeof(ISirenOfShameDevice))]
    public class MockSirenOfShameDevice : ISirenOfShameDevice
    {
        private readonly MockSirenOfShameDeviceDialog _dlg;
        private bool _isConnected;

        public int FirmwareVersion
        {
            get { return 1; }
        }

        public int HardwareVersion
        {
            get { return 1; }
        }

        public HardwareType HardwareType
        {
            get { return HardwareType.Pro; }
        }

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event UploadProgressEventHandler UploadProgress;
        public event UploadCompletedEventHandler UploadCompleted;

        protected virtual void InvokeOnUploadProgress(int value)
        {
            UploadProgressEventHandler handler = UploadProgress;
            if (handler != null) handler(this, new UploadProgressEventHandlerArgs { Value = value });
        }

        protected virtual void InvokeOnUploadCompleted(AggregateException exception)
        {
            UploadCompletedEventHandler handler = UploadCompleted;
            if (handler != null) handler(this, new UploadCompletedEventHandlerArgs { Exception = exception });
        }

        public MockSirenOfShameDevice()
        {
            _dlg = new MockSirenOfShameDeviceDialog();
            _dlg.ConnectedChanged += _dlg_ConnectedChanged;
            _dlg.Left = 0;
            _dlg.Top = 0;
            _dlg.Show();
        }

        void _dlg_ConnectedChanged(object sender, EventArgs e)
        {
            if (IsConnected && !_dlg.Connected)
            {
                _isConnected = false;
                if (Disconnected != null)
                {
                    Disconnected(this, new EventArgs());
                }
            }
            else if (!IsConnected && _dlg.Connected)
            {
                _isConnected = true;
                if (Connected != null)
                {
                    Connected(this, new EventArgs());
                }
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
        }

        public IEnumerable<AudioPattern> AudioPatterns
        {
            get { return _dlg.AudioPatterns; }
        }

        public IEnumerable<LedPattern> LedPatterns
        {
            get { return _dlg.LedPatterns; }
        }

        public void PlayAudioPattern(AudioPattern pattern, TimeSpan? duration)
        {
            _dlg.SetAudio(pattern, duration);
        }

        public void StopAudioPattern()
        {
            PlayAudioPattern(null, null);
        }

        public void PlayLightPattern(LedPattern pattern, TimeSpan? duration)
        {
            _dlg.SetLight(pattern, duration);
        }

        public void StopLightPattern()
        {
            PlayLightPattern(null, null);
        }

        public void WndProc(ref Message message)
        {

        }

        public Task UploadCustomPatternsAsync(SirenOfShameSettings settings, IList<AudioPatternSetting> audioPatterns, IList<UploadLedPattern> ledPatterns)
        {
            var task = Task.Factory.StartNew(() => UploadCustomPatterns(audioPatterns, ledPatterns, InvokeOnUploadProgress));
            task.ContinueWith(t => InvokeOnUploadCompleted(null), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(t => InvokeOnUploadCompleted(t.Exception), TaskContinuationOptions.OnlyOnRanToCompletion);
            return task;
        }

        public void UploadCustomPatterns(IEnumerable<AudioPatternSetting> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns, Action<int> progressFunc)
        {
            _dlg.AudioPatternText = audioPatterns.Select(p => p.Name).JoinAsString("\n");
            _dlg.LedPatternText = ledPatterns.Select(p => p.Name).JoinAsString("\n");

            for (int i = 0; i < 100; i++)
            {
                progressFunc(i);
                Thread.Sleep(20);
            }
        }

        public bool TryConnect()
        {
            _isConnected = true;
            if (Connected != null)
            {
                Connected(this, new EventArgs());
            }
            return true;
        }

        public void Disconnect()
        {
            _isConnected = false;
            if (Disconnected != null)
            {
                Disconnected(this, new EventArgs());
            }
        }

        public void ManualControl(ManualControlData manualControlData)
        {
        }

        public void PerformFirmwareUpgrade(Stream hexFileStream, Action<int> progressFunc)
        {

        }

        public SirenOfShameInfo ReadDeviceInfo()
        {
            UsbInfoPacket infoPacket = new UsbInfoPacket
            {
                AudioMode = (byte)_dlg.CurrentAudioPattern.Id,
                AudioPlayDuration = (ushort)(_dlg.CurrentAudioDuration == null ? 0 : _dlg.CurrentAudioDuration.Value.TotalMilliseconds / 100),
                LedMode = (byte)_dlg.CurrentLightPattern.Id,
                LedPlayDuration = (ushort)(_dlg.CurrentLightDuration == null ? 0 : _dlg.CurrentLightDuration.Value.TotalMilliseconds / 100),
                ExternalMemorySize = 0,
                HardwareType = HardwareType.Pro,
                FirmwareVersion = 1
            };
            return new SirenOfShameInfo(infoPacket);
        }
    }
}
