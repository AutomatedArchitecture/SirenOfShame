using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SirenOfShame.Lib.Device.SdCardFileSystem;

namespace SirenOfShame.Lib.Device
{
    public interface ISirenOfShameDevice
    {
        bool IsConnected { get; }
        IEnumerable<AudioPattern> AudioPatterns { get; }
        IEnumerable<LedPattern> LedPatterns { get; }
        event EventHandler Connected;
        event EventHandler Disconnected;
        void SetAudio(AudioPattern pattern, TimeSpan? duration);
        void SetLight(LedPattern pattern, TimeSpan? duration);
        void WndProc(ref Message message);
        void UploadCustomPatterns(IEnumerable<UploadAudioPattern> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns, Action<int> progressFunc);
        void TryConnect();
        void PerformFirmwareUpgrade(Stream hexFileStream, Action<int> progressFunc);
        SirenOfShameInfo ReadDeviceInfo();
        void Disconnect();
    }
}