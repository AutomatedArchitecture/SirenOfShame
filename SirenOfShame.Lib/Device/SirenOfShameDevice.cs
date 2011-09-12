using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using TeensyHidBootloaderLib;
using UsbLib;

namespace SirenOfShame.Lib.Device
{
    [Export(typeof(ISirenOfShameDevice))]
    public class SirenOfShameDevice : ISirenOfShameDevice
    {
        public const int LedPatternBufferSize = 24;
        public const int AudioPatternBufferSize = 32;
        public const int AudioSampleRate = 7650;

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SirenOfShameDevice));
        public const string VendorId = "16d0";
        public const string ProductId = "0646";
        public const int ReportId_Out_ControlPacket = 1;
        public const int ReportId_Out_Upload = 2;
        public const int ReportId_In_Info = 1;
        public const int ReportId_In_ReadAudioPacket = 3;
        public const int ReportId_In_ReadLedPacket = 4;

        public const int AudioSampleSize = 8000;
        private readonly List<AudioPattern> _audioPatterns = new List<AudioPattern>();
        private readonly List<LedPattern> _ledPatterns = new List<LedPattern>();
        private const int PacketSize = 1 + 37; // report id + packet length
        private readonly UsbDeviceNotification _usbDeviceNotification = new UsbDeviceNotification();

        private DeviceInterfaceFile _deviceInterfaceFile;
        private byte[] _buffer;
        private bool _connecting;
        private bool _disconnecting;
        private const byte LED_MODE_MANUAL = 1;
        private const UInt16 Duration_Forever = 0xfffe;

        public bool IsConnected { get; private set; }
        public int FirmwareVersion { get; private set; }
        public int HardwareVersion { get; private set; }
        public HardwareType HardwareType { get; private set; }

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public SirenOfShameDevice()
        {
            _usbDeviceNotification.UsbDeviceArrived += _usbDeviceNotification_UsbDeviceArrived;
        }

        private void _usbDeviceNotification_UsbDeviceArrived(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private DeviceInterface FindDevice()
        {
            Guid hidGuid = DeviceInformationSet.GetHidGuid();
            using (var deviceInformationSet = new DeviceInformationSet(hidGuid, DiGetClassFlags.Present | DiGetClassFlags.DeviceInterface))
            {
                DeviceInterface deviceInterface = deviceInformationSet.GetDeviceInterfaces(hidGuid)
                    .FirstOrDefault(dis => dis.Details.DevicePath.Contains(VendorId) && dis.Details.DevicePath.Contains(ProductId));
                if (deviceInterface == null)
                {
                    _log.Debug("device not found");
                }
                else
                {
                    _log.Debug("device found: " + deviceInterface.Details.DevicePath);
                }
                return deviceInterface;
            }
        }

        private void CheckConnection()
        {
            if (_disconnecting)
            {
                return;
            }
            DeviceInterface deviceInterface = FindDevice();
            if (IsConnected && deviceInterface == null)
            {
                OnDisconnected();
            }
            else if (IsConnected && deviceInterface != null)
            {
                // still connected
            }
            else if (!IsConnected && deviceInterface != null)
            {
                TryConnect();
            }
        }

        public bool TryConnect()
        {
            _disconnecting = false;
            if (_connecting)
            {
                return true;
            }
            _connecting = true;
            try
            {
                DeviceInterface deviceInterface = FindDevice();
                if (deviceInterface == null)
                {
                    return false;
                }
                Thread.Sleep(500);
                _deviceInterfaceFile = deviceInterface.OpenFile(PacketSize);
                var caps = _deviceInterfaceFile.Capabilities;
                _log.Debug("InputReportByteLength: " + caps.InputReportByteLength);
                _log.Debug("OutputReportByteLength: " + caps.NumberOutputDataIndices);
                _log.Debug("OutputReportByteLength: " + caps.OutputReportByteLength);
                BeginAsyncRead();
                ReadDeviceInfo();
                ReadAudioPatterns();
                ReadLedPatterns();
                OnConnected();
                return true;
            }
            finally
            {
                _connecting = false;
            }
        }

        public void Disconnect()
        {
            OnDisconnected();
        }

        public void ManualControl(ManualControlData manualControlData)
        {
            byte manualSiren = (byte)(manualControlData.Siren ? 1 : 0);
            SendControlPacket(
                ledMode: LED_MODE_MANUAL,
                audioMode: manualSiren,
                manualLeds0: manualControlData.Led0,
                manualLeds1: manualControlData.Led1,
                manualLeds2: manualControlData.Led2,
                manualLeds3: manualControlData.Led3,
                manualLeds4: manualControlData.Led4);
        }

        public void PerformFirmwareUpgrade(Stream hexFileStream, Action<int> progressFunc)
        {
            _log.Info("Starting firmware upgrade");
            TryConnect();
            if (IsConnected)
            {
                SendControlPacket(controlByte: ControlByte1Flags.FirmwareUpgrade);
            }
            progressFunc(10);
            var programmer = new TeensyHidBootloaderProgrammer(McuType.ATMega32u2);
            programmer.Program(hexFileStream, true, true, new TimeSpan(0, 0, 1, 0), i => progressFunc(10 + (int)(i * 90.0 / 100.0)));
        }

        public SirenOfShameInfo ReadDeviceInfo()
        {
            UsbInfoPacket infoPacket = _deviceInterfaceFile.GetInputReport<UsbInfoPacket>(ReportId_In_Info, PacketSize);
            _log.Debug("Info packet receieved:");
            _log.Debug("\tVersion: " + infoPacket.FirmwareVersion);
            _log.Debug("\tHardwareType: " + infoPacket.HardwareType);
            _log.Debug("\tHardwareVersion: " + infoPacket.HardwareVersion);
            _log.Debug("\tExternalMemorySize: " + infoPacket.ExternalMemorySize);
            _log.Debug("\tAudioMode: " + infoPacket.AudioMode);
            _log.Debug("\tAudioPlayDuration: " + infoPacket.AudioPlayDuration);
            _log.Debug("\tLedMode: " + infoPacket.LedMode);
            _log.Debug("\tLedPlayDuration: " + infoPacket.LedPlayDuration);
            FirmwareVersion = infoPacket.FirmwareVersion;
            HardwareType = infoPacket.HardwareType;
            HardwareVersion = infoPacket.HardwareVersion;
            return new SirenOfShameInfo(infoPacket);
        }

        private void ReadAudioPatterns()
        {
            _audioPatterns.Clear();
            SendControlPacket(readAudioIndex: 0);
            while (true)
            {
                UsbReadAudioPacket audioPatternPacket = _deviceInterfaceFile.GetInputReport<UsbReadAudioPacket>(ReportId_In_ReadAudioPacket, PacketSize);
                if (audioPatternPacket.Id == 0xff)
                {
                    return;
                }
                _audioPatterns.Add(new AudioPattern
                {
                    Id = audioPatternPacket.Id,
                    Name = new string(audioPatternPacket.Name).TrimEnd('\0')
                });
            }
        }

        private void ReadLedPatterns()
        {
            _ledPatterns.Clear();
            SendControlPacket(readLedIndex: 0);
            while (true)
            {
                UsbReadLedPacket ledPatternPacket = _deviceInterfaceFile.GetInputReport<UsbReadLedPacket>(ReportId_In_ReadLedPacket, PacketSize);
                if (ledPatternPacket.Id == 0xff)
                {
                    return;
                }
                _ledPatterns.Add(new LedPattern
                {
                    Id = ledPatternPacket.Id,
                    Name = new string(ledPatternPacket.Name).TrimEnd('\0')
                });
            }
        }

        private void OnDisconnected()
        {
            try
            {
                _disconnecting = true;
                IsConnected = false;
                if (_deviceInterfaceFile != null)
                {
                    _deviceInterfaceFile.Dispose();
                    _deviceInterfaceFile = null;
                    Thread.Sleep(500);
                }
                if (Disconnected != null)
                {
                    Disconnected(this, new EventArgs());
                }
            }
            finally
            {
                _disconnecting = false;
            }
        }

        private void OnConnected()
        {
            IsConnected = true;
            if (Connected != null)
            {
                Connected(this, new EventArgs());
            }
        }

        public IEnumerable<AudioPattern> AudioPatterns
        {
            get { return _audioPatterns; }
        }

        public IEnumerable<LedPattern> LedPatterns
        {
            get { return _ledPatterns; }
        }

        public void UploadCustomPatterns(IEnumerable<UploadAudioPattern> audioPatterns, IEnumerable<UploadLedPattern> ledPatterns, Action<int> progressFunc)
        {
            EnsureConnected();
            progressFunc(10);

            FileSystemBuilder fileSystemBuilder = new FileSystemBuilder();
            using (Stream fileSystemStream = fileSystemBuilder.Build(audioPatterns, ledPatterns))
            {
                // set the audio and led pattern count to 0 for the first run so that if the upload fails
                // the returned names will not be corrupt
                fileSystemStream.Position = 0;
                fileSystemBuilder.WriteHeader(fileSystemStream, 0, 0);

                fileSystemStream.Position = 0;
                for (int address = 0; address < fileSystemStream.Length; address += 32)
                {
                    SendData(address, fileSystemStream);
                    double progress = (double)address / fileSystemStream.Length;
                    progressFunc((int)(10 + (progress * 80.0)));
                }
                progressFunc(90);

                // set the real audio and led pattern count
                fileSystemStream.Position = 0;
                fileSystemBuilder.WriteHeader(fileSystemStream, audioPatterns.Count(), ledPatterns.Count());
                fileSystemStream.Position = 0;
                SendData(0, fileSystemStream);
                progressFunc(100);
            }
        }

        private void SendData(int address, Stream stream)
        {
            byte[] buffer = new byte[PacketSize];
            stream.Read(buffer, 5, 32);
            buffer[0] = ReportId_Out_Upload;
            buffer[1] = (byte)((address >> 0) & 0xff);
            buffer[2] = (byte)((address >> 8) & 0xff);
            buffer[3] = (byte)((address >> 16) & 0xff);
            buffer[4] = (byte)((address >> 24) & 0xff);
            _deviceInterfaceFile.SetOutputReport(buffer);
        }

        public void StopAudioPattern()
        {
            PlayAudioPattern(null, null);
        }

        public void PlayLightPattern(LedPattern lightPattern, TimeSpan? durationTimeSpan)
        {
            EnsureConnected();
            if (lightPattern == null)
            {
                SendControlPacket(ledMode: 0, ledDuration: 0);
            }
            else
            {
                UInt16 duration = CalculateDurationFromTimeSpan(durationTimeSpan);
                SendControlPacket(ledMode: (byte)lightPattern.Id, ledDuration: duration);
            }
        }

        public void StopLightPattern()
        {
            PlayLightPattern(null, null);
        }

        public void PlayAudioPattern(AudioPattern audioPattern, TimeSpan? durationTimeSpan)
        {
            EnsureConnected();
            if (audioPattern == null)
            {
                SendControlPacket(audioMode: 0, audioDuration: 0);
            }
            else
            {
                UInt16 duration = CalculateDurationFromTimeSpan(durationTimeSpan);
                SendControlPacket(audioMode: (byte)audioPattern.Id, audioDuration: duration);
            }
        }

        private UInt16 CalculateDurationFromTimeSpan(TimeSpan? durationTimeSpan)
        {
            if (durationTimeSpan == null)
            {
                return Duration_Forever;
            }
            UInt32 result = (UInt32)(durationTimeSpan.Value.TotalSeconds * 10.0);
            if (result > UInt16.MaxValue - 10)
            {
                return Duration_Forever;
            }
            return (UInt16)result;
        }

        public void WndProc(ref Message message)
        {
            _usbDeviceNotification.WndProc(ref message);
        }

        private void BeginAsyncRead()
        {
            _buffer = new byte[PacketSize];
            _deviceInterfaceFile.Stream.BeginRead(_buffer, 0, _buffer.Length, OnRead, _buffer);
        }

        private void OnRead(IAsyncResult ar)
        {
            try
            {
                int bytesRead = _deviceInterfaceFile.Stream.EndRead(ar);
                _log.Debug("Read " + bytesRead + "bytes");
                BeginAsyncRead();
            }
            catch (Exception ex)
            {
                _log.Error("read failed", ex);
                try
                {
                    CheckConnection();
                }
                catch (Exception ex2)
                {
                    _log.Error("CheckConnection", ex2);
                }
            }
        }

        private void SendControlPacket(
            ControlByte1Flags controlByte = ControlByte1Flags.Ignore,
            byte audioMode = (byte)0xff, UInt16 audioDuration = (UInt16) 0xffff,
            byte ledMode = (byte)0xff, UInt16 ledDuration = (UInt16)0xffff,
            byte readAudioIndex = (byte)0xff,
            byte readLedIndex = (byte)0xff,
            byte manualLeds0 = (byte)0xff,
            byte manualLeds1 = (byte)0xff,
            byte manualLeds2 = (byte)0xff,
            byte manualLeds3 = (byte)0xff,
            byte manualLeds4 = (byte)0xff)
        {
            UsbControlPacket usbControlPacket = new UsbControlPacket
            {
                ReportId = ReportId_Out_ControlPacket,
                ControlByte1 = controlByte,
                AudioMode = audioMode,
                AudioDuration = audioDuration,
                LedMode = ledMode,
                LedDuration = ledDuration,
                ReadAudioIndex = readAudioIndex,
                ReadLedIndex = readLedIndex,
                ManualLeds0 = manualLeds0,
                ManualLeds1 = manualLeds1,
                ManualLeds2 = manualLeds2,
                ManualLeds3 = manualLeds3,
                ManualLeds4 = manualLeds4
            };
            _deviceInterfaceFile.SetOutputReport(usbControlPacket, PacketSize);
        }

        private void EnsureConnected()
        {
            if (IsConnected)
            {
                return;
            }
            CheckConnection();
            if (!IsConnected)
            {
                throw new Exception("Could not get connection to device");
            }
        }
    }
}
