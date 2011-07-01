using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Device
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class UsbReadAudioPacket
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte Id;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public char[] Name;
    }
}
