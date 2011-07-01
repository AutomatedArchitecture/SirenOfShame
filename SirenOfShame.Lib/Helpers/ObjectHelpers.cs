using System;
using System.Runtime.InteropServices;

namespace SirenOfShame.Lib.Helpers
{
    public static class ObjectHelpers
    {
        public static byte[] SerializeToBytes(this object s)
        {
            int objSize = Marshal.SizeOf(s);
            byte[] buffer = new byte[objSize];
            IntPtr ptr = Marshal.AllocHGlobal(objSize);
            try
            {
                Marshal.StructureToPtr(s, ptr, false);
                Marshal.Copy(ptr, buffer, 0, objSize);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return buffer;
        }

        public static T Deserialize<T>(byte[] data, int offset)
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            Array.Copy(data, offset, buffer, 0, buffer.Length);
            GCHandle pinnedPacket = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                T result = (T)Marshal.PtrToStructure(pinnedPacket.AddrOfPinnedObject(), typeof(T));
                return result;
            }
            finally
            {
                pinnedPacket.Free();
            }
        }
    }
}
