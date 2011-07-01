using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UsbLib
{
    public class DeviceInformationSet : IDisposable
    {
        private IntPtr _handle;
        private readonly IntPtr _invalidHandleValue = new IntPtr(-1);

        [DllImport("hid.dll", SetLastError = true)]
        static extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,
            [MarshalAs(UnmanagedType.LPTStr)] string Enumerator,
            IntPtr hwndParent,
            uint Flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           IntPtr devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref DeviceInterfaceData deviceInterfaceData
        );

        public DeviceInformationSet(Guid classGuid, DiGetClassFlags flags)
        {
            // We start at the "root" of the device tree and look for all
            // devices that match the interface GUID of a disk
            _handle = SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, (uint)(flags));
            if (_handle == _invalidHandleValue)
            {
                var lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, "SetupDiGetClassDevs failed");
            }
        }

        public IEnumerable<DeviceInterface> GetDeviceInterfaces(Guid interfaceClassGuid)
        {
            bool success = true;
            int i = 0;
            while (success)
            {
                // create a Device Interface Data structure
                DeviceInterfaceData dia = new DeviceInterfaceData();
                dia.cbSize = Marshal.SizeOf(dia);

                // start the enumeration 
                success = SetupDiEnumDeviceInterfaces(_handle, IntPtr.Zero, ref interfaceClassGuid, (uint)i, ref dia);
                if (success)
                {
                    yield return new DeviceInterface(_handle, dia);
                }
                i++;
            }
        }

        public void Dispose()
        {
            if (_handle != _invalidHandleValue)
            {
                SetupDiDestroyDeviceInfoList(_handle);
                _handle = _invalidHandleValue;
            }
        }

        public static Guid GetHidGuid()
        {
            Guid hidGuid = default(Guid);
            HidD_GetHidGuid(ref hidGuid);
            return hidGuid;
        }
    }
}
