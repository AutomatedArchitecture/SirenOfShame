using System.Runtime.InteropServices;

namespace UsbLib
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DeviceInterfaceDetailData
    {
        public const int BufferSize = 255;

        public int cbSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BufferSize)]
        public string DevicePath;
    }
}