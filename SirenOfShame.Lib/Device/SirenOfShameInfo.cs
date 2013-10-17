using System;
using System.Text;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib.Device
{
    public class SirenOfShameInfo
    {
        /// <summary>
        /// <see cref="AudioMode"/> indicating the audio is currently off.
        /// </summary>
        public const byte AudioModeOff = 0;

        /// <summary>
        /// <see cref="LedMode"/> indicating the LEDs are currently off.
        /// </summary>
        public const byte LedModeOff = 0;

        /// <summary>
        /// <see cref="LedMode"/> indicating the LEDs are currently in manual mode.
        /// </summary>
        public const byte LedModeManual = 1;

        /// <summary>
        /// The firmware version of the connected device.
        /// </summary>
        public ushort FirmwareVersion { get; private set; }

        /// <summary>
        /// The type of hardware of the connected device. See <see cref="HardwareType"/>.
        /// </summary>
        public HardwareType HardwareType { get; private set; }

        /// <summary>
        /// The hardware version of the connected device. This value will not change unless you buy new hardware.
        /// </summary>
        public byte HardwareVersion { get; private set; }
        
        /// <summary>
        /// The current audio mode. <see cref="AudioModeOff"/> if the audio is off. >=1 if the audio is currently playing.
        /// </summary>
        public byte AudioMode { get; private set; }

        /// <summary>
        /// The time remaining on the current audio output.
        /// </summary>
        public TimeSpan AudioPlayDuration { get; private set; }

        /// <summary>
        /// The current LED mode. <see cref="LedModeOff"/> if the LEDs are off. <see cref="LedModeManual"/> if the LEDs are being
        /// controlled manually. >=2 if the LEDs are currently playing a pattern.
        /// </summary>
        public byte LedMode { get; private set; }

        /// <summary>
        /// The time remaining on the current LED output.
        /// </summary>
        public TimeSpan LedPlayDuration { get; private set; }

        /// <summary>
        /// If this is a <see cref="HardwareType"/>.Pro model this will return the number of bytes to store additional LED and audio
        /// patterns. If this is a <see cref="HardwareType"/>.Standard model this will return 0.
        /// </summary>
        public uint ExternalMemorySize { get; set; }

        internal SirenOfShameInfo(UsbInfoPacket infoPacket)
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(this.GetType().Name + ":");
            stringBuilder.AppendLine("\tFirmwareVersion: " + this.FirmwareVersion);
            stringBuilder.AppendLine("\tHardwareType: " + this.HardwareType );
            stringBuilder.AppendLine("\tHardwareVersion: " + this.HardwareVersion );
            stringBuilder.AppendLine("\tAudioMode: " + this.AudioMode );
            stringBuilder.AppendLine("\tAudioPlayDuration: " + this.AudioPlayDuration );
            stringBuilder.AppendLine("\tLedMode: " + this.LedMode );
            stringBuilder.AppendLine("\tLedPlayDuration: " + this.LedPlayDuration );
            stringBuilder.AppendLine("\tExternal Memory Size: " + SiUnitHelpers.ToBinaryString(this.ExternalMemorySize) + "B");
            return stringBuilder.ToString();
        }
    }
}
