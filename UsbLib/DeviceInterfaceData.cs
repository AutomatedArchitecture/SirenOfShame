using System;
using System.Runtime.InteropServices;

namespace UsbLib
{
    // ReSharper disable FieldCanBeMadeReadOnly.Local
    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceInterfaceData
    {
        public Int32 cbSize;
        public Guid interfaceClassGuid;
        public Int32 flags;
        private UIntPtr reserved;
    }
    // ReSharper restore FieldCanBeMadeReadOnly.Local
}