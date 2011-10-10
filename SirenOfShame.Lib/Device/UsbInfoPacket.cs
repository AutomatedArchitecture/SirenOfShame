using System;
using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Device
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbInfoPacket
    {
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 FirmwareVersion;
        
        [MarshalAs(UnmanagedType.U1)]
        public HardwareType HardwareType;

        [MarshalAs(UnmanagedType.U1)]
        public byte HardwareVersion;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ExternalMemorySize;

        [MarshalAs(UnmanagedType.U1)]
        public byte AudioMode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 AudioPlayDuration;

        [MarshalAs(UnmanagedType.U1)]
        public byte LedMode;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 LedPlayDuration;
    }
}