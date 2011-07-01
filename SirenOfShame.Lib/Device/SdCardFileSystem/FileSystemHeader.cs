using System;
using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Device.SdCardFileSystem
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileSystemHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] SoS;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 LedPatCount;

        [MarshalAs(UnmanagedType.U2)]
        public UInt16 AudioPatCount;
    }
}