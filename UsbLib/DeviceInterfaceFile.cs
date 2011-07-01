using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace UsbLib
{
    public class DeviceInterfaceFile : IDisposable
    {
        private SafeFileHandle _fileHandle;
        private readonly FileStream _stream;
        private IntPtr _preparsedData;
        private readonly TimeSpan _defaultRetryDuration = new TimeSpan(0, 0, 0, 2);

        public FileStream Stream
        {
            get { return _stream; }
        }

        public HidCaps Capabilities
        {
            get
            {
                HidCaps capabilities;
                HidP_GetCaps(_preparsedData, out capabilities);
                return capabilities;
            }
        }

        [DllImport("hid.dll", SetLastError = true)]
        static extern int HidP_GetCaps(IntPtr preparsedData, out HidCaps capabilities);

        [DllImport("hid.dll", SetLastError = true)]
        static extern Boolean HidD_GetPreparsedData(IntPtr hFileHandle, out IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        static extern Boolean HidD_FreePreparsedData(IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        static extern Boolean HidD_SetOutputReport(IntPtr hFileHandle, byte[] reportBuffer, uint reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        static extern Boolean HidD_GetInputReport(IntPtr hFileHandle, byte[] reportBuffer, uint reportBufferLength);

        public DeviceInterfaceFile(SafeFileHandle fileHandle, FileStream stream)
        {
            _fileHandle = fileHandle;
            _stream = stream;

            if (!HidD_GetPreparsedData(fileHandle.DangerousGetHandle(), out _preparsedData))
            {
                var lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, "HidD_GetPreparsedData failed");
            }
        }

        public void Dispose()
        {
            if (_preparsedData != IntPtr.Zero)
            {
                HidD_FreePreparsedData(_preparsedData);
                _preparsedData = IntPtr.Zero;
            }
            if (_fileHandle != null)
            {
                _fileHandle.Close();
                _fileHandle = null;
            }
        }

        public T GetInputReport<T>(byte? reportId, int packetSize)
        {
            byte[] buffer = new byte[packetSize];
            GetInputReport(reportId, buffer);

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

        public void GetInputReport(byte? reportId, byte[] reportBuffer)
        {
            GetInputReport(reportId, reportBuffer, _defaultRetryDuration);
        }

        public void GetInputReport(byte? reportId, byte[] reportBuffer, TimeSpan retryDuration)
        {
            if (reportId == null)
            {
                reportBuffer[0] = 0;
            }
            else
            {
                reportBuffer[0] = reportId.Value;
            }
            DateTime start = DateTime.Now;
            do
            {
                if (HidD_GetInputReport(_fileHandle.DangerousGetHandle(), reportBuffer, (uint)reportBuffer.Length))
                {
                    return;
                }
            } while (DateTime.Now - start < retryDuration);
            var lastError = Marshal.GetLastWin32Error();
            throw new Win32Exception(lastError, "HidD_SetOutputReport failed. Timeout");
        }

        public void SetOutputReport(byte[] reportBuffer)
        {
            SetOutputReport(reportBuffer, _defaultRetryDuration);
        }

        public void SetOutputReport(byte[] reportBuffer, TimeSpan retryDuration)
        {
            DateTime start = DateTime.Now;
            do
            {
                if (HidD_SetOutputReport(_fileHandle.DangerousGetHandle(), reportBuffer, (uint)reportBuffer.Length))
                {
                    return;
                }
            } while (DateTime.Now - start < retryDuration);
            var lastError = Marshal.GetLastWin32Error();
            throw new Win32Exception(lastError, "HidD_SetOutputReport failed");
        }

        public void SetOutputReport(object obj, int packetSize)
        {
            byte[] buffer = new byte[packetSize];
            int objSize = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(objSize);
            try
            {
                Marshal.StructureToPtr(obj, ptr, false);
                Marshal.Copy(ptr, buffer, 0, objSize);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            SetOutputReport(buffer);
        }
    }
}
