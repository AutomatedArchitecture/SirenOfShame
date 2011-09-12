using System;

namespace SirenOfShame.Lib.Device
{
    public class SirenOfShameInfo
    {
        public ushort FirmwareVersion { get; private set; }
        public HardwareType HardwareType { get; private set; }
        public byte HardwareVersion { get; private set; }
        public byte AudioMode { get; private set; }
        public TimeSpan AudioPlayDuration { get; private set; }
        public byte LedMode { get; private set; }
        public TimeSpan LedPlayDuration { get; private set; }
        public uint ExternalMemorySize { get; set; }

        public SirenOfShameInfo(UsbInfoPacket infoPacket)
        {
            FirmwareVersion = infoPacket.FirmwareVersion;
            HardwareType = infoPacket.HardwareType;
            HardwareVersion = infoPacket.HardwareVersion;
            AudioMode = infoPacket.AudioMode;
            AudioPlayDuration = new TimeSpan(0, 0, 0, 0, infoPacket.AudioPlayDuration * 100);
            LedMode = infoPacket.LedMode;
            LedPlayDuration = new TimeSpan(0, 0, 0, 0, infoPacket.LedPlayDuration * 100);
            ExternalMemorySize = infoPacket.ExternalMemorySize;
        }
    }
}
