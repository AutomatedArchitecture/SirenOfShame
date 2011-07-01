using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UsbLib
{
    public class DeviceInterfaceDetail
    {
        private readonly DeviceInterfaceDetailData _didd;
        private readonly DevInfoData _deviceInfoData;

        public string DevicePath
        {
            get { return _didd.DevicePath; }
        }

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref DeviceInterfaceData deviceInterfaceData,
           ref DeviceInterfaceDetailData deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           ref DevInfoData deviceInfoData
        );

        public DeviceInterfaceDetail(IntPtr handle, DeviceInterfaceData did)
        {
            // build a DevInfo Data structure
            _deviceInfoData = new DevInfoData();
            _deviceInfoData.cbSize = (uint)Marshal.SizeOf(_deviceInfoData);

            // build a Device Interface Detail Data structure
            _didd = new DeviceInterfaceDetailData();
            if (IntPtr.Size == 8) // for 64 bit operating systems
                _didd.cbSize = 8;
            else
                _didd.cbSize = 4 + Marshal.SystemDefaultCharSize; // for 32 bit systems

            // now we can get some more detailed information
            uint nRequiredSize;
            const uint nBytes = DeviceInterfaceDetailData.BufferSize;
            if (!SetupDiGetDeviceInterfaceDetail(handle, ref did, ref _didd, nBytes, out nRequiredSize, ref _deviceInfoData))
            {
                var lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, "SetupDiGetDeviceInterfaceDetail failed");
            }
        }
    }
}
