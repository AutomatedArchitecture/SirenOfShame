using System;
using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Device
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbControlPacket
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte ReportId;

        [MarshalAs(UnmanagedType.U1)]
        public ControlByte1Flags ControlByte1;

        [MarshalAs(UnmanagedType.U1)]
        public byte AudioMode;

        [MarshalAs(UnmanagedType.U1)]
        public byte LedMode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 AudioDuration;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 LedDuration;

        [MarshalAs(UnmanagedType.U1)]
        public byte ReadAudioIndex;

        [MarshalAs(UnmanagedType.U1)]
        public byte ReadLedIndex;

        [MarshalAs(UnmanagedType.U1)]
        public byte ManualLeds;

        [MarshalAs(UnmanagedType.U1)]
        public byte ManualSiren;
    }

    [Flags]
    public enum ControlByte1Flags : byte
    {
        Ignore = 0xff,
        FirmwareUpgrade = 0x01,
        EchoOn = 0x02,
        Debug = 0x04
    }
}
