using System;
using System.Runtime.InteropServices;

namespace UsbLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DevInfoData
    {
        public uint cbSize;
        public Guid ClassGuid;
        public uint DevInst;
        public IntPtr Reserved;
    }
}