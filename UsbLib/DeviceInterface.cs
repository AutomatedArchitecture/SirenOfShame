using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace UsbLib
{
    public class DeviceInterface
    {
        private readonly DeviceInterfaceData _did;
        private readonly IntPtr _handle;
        private readonly DeviceInterfaceDetail _details;
        private readonly bool _isValidUsbDevice;

        public DeviceInterfaceData DeviceInterfaceData
        {
            get { return _did; }
        }

        public DeviceInterface(IntPtr handle, DeviceInterfaceData did)
        {
            _handle = handle;
            _did = did;
            try
            {
                // On Windows 8 we see exceptions with getting the device info when a bluetooth device is connected.
                _details = new DeviceInterfaceDetail(_handle, _did);
                _isValidUsbDevice = true;
            }
            catch (Exception)
            {
                _isValidUsbDevice = false;
            }
        }

        public bool IsValidUsbDevice { get { return _isValidUsbDevice; } }

        public DeviceInterfaceDetail Details { get { return _details; } }

        public DeviceInterfaceFile OpenFile(int bufferSize)
        {
            var handle = PInvoke.CreateFile(Details.DevicePath,
                        (uint)PInvoke.EFileAccess.GenericRead | (uint)PInvoke.EFileAccess.GenericWrite,
                        (uint)PInvoke.EFileShare.Read | (uint)PInvoke.EFileShare.Write, IntPtr.Zero,
                        (uint)PInvoke.ECreationDisposition.OpenExisting,
                        (uint)PInvoke.EFileAttributes.Overlapped, IntPtr.Zero);
            if (handle.IsInvalid)
            {
                var lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, "CreateFile failed");
            }
            FileStream stream = new FileStream(handle, FileAccess.Read | FileAccess.Write, bufferSize, true);
            return new DeviceInterfaceFile(handle, stream);
        }
    }
}
