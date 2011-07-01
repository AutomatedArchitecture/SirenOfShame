using System;
using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbLedFatPacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Name;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 Addr;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 Length;
    }
}