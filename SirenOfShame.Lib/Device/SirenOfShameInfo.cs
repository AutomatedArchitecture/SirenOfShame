using System;

namespace SirenOfShame.Lib.Device
{
    public class SirenOfShameInfo
    {
        public ushort Version { get; private set; }
        public HardwareType HardwareType { get; private set; }
        public byte AudioMode { get; private set; }
        public TimeSpan AudioPlayDuration { get; private set; }
        public byte LedMode { get; private set; }
        public TimeSpan LedPlayDuration { get; private set; }

        public SirenOfShameInfo(UsbInfoPacket infoPacket)
        {
            Version = infoPacket.Version;
            HardwareType = infoPacket.HardwareType;
            AudioMode = infoPacket.AudioMode;
            AudioPlayDuration = new TimeSpan(0, 0, 0, 0, infoPacket.AudioPlayDuration * 100);
            LedMode = infoPacket.LedMode;
            LedPlayDuration = new TimeSpan(0, 0, 0, 0, infoPacket.LedPlayDuration * 100);
        }
    }
}
