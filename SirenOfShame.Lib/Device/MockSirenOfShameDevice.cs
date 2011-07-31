using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib.Device
{
    [Export(typeof(ISirenOfShameDevice))]
    public class MockSirenOfShameDevice : ISirenOfShameDevice
    {
        private readonly MockSirenOfShameDeviceDialog _dlg;
        private bool _isConnected;

        public int Version
        {
            get { return 1; }
        }

        public HardwareType HardwareType
        {
            get { return HardwareType.Pro; }
        }

        public event EventHandler Connected;
        public event EventHandler Disconnected;

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

        public void SetAudio(AudioPattern pattern, TimeSpan? duration)
        {
            _dlg.SetAudio(pattern, duration);
        }

        public void SetLight(LedPattern pattern, TimeSpan? duration)
        {
            _dlg.SetLight(pattern, duration);
        }

        public void WndProc(ref Message message)
        {

        }

        public void UploadCustomPatterns(IEnumerable<UploadAudioPattern> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns, Action<int> progressFunc)
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
                Version = 1
            };
            return new SirenOfShameInfo(infoPacket);
        }
    }
}
