using System;
using System.Linq;
using System.Threading;
using log4net;
using UsbLib;

namespace TeensyHidBootloaderLib
{
    public class TeensyDevice : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IntelHexFile));
        private DeviceInterfaceFile _handle;

        public TeensyDevice(bool waitForDeviceToAppear, int blockSize, TimeSpan timeOut)
        {
            bool waited = false;

            DateTime start = DateTime.Now;
            while (true)
            {
                if (TryOpen(blockSize)) break;
                if (!waitForDeviceToAppear) throw new Exception("Unable to open device");
                if (!waited)
                {
                    Log.Debug("Waiting for Teensy device...");
                    Log.Debug(" (hint: press the reset button)");
                    waited = true;
                }
                if (DateTime.Now - start > timeOut)
                {
                    throw new Exception("Timeout waiting for device");
                }
                Thread.Sleep(250);
            }
            Log.Debug("Found HalfKay Bootloader");
        }

        private bool TryOpen(int blockSize)
        {
            Dispose();

            DeviceInterface deviceInterface;
            Guid hidGuid = DeviceInformationSet.GetHidGuid();
            using (var deviceInformationSet = new DeviceInformationSet(hidGuid, DiGetClassFlags.Present | DiGetClassFlags.DeviceInterface))
            {
                deviceInterface = deviceInformationSet.GetDeviceInterfaces(hidGuid)
                    .Where(d => d.IsValidUsbDevice)
                    .FirstOrDefault(dis => (dis.Details.DevicePath.Contains("16c0") && dis.Details.DevicePath.Contains("0478"))
                        || (dis.Details.DevicePath.Contains("03eb") && dis.Details.DevicePath.Contains("2067")));
                if (deviceInterface != null)
                {
                    _handle = deviceInterface.OpenFile(blockSize + 3);
                    return true;
                }

                return false;
            }
        }

        public void Dispose()
        {
            if (_handle != null)
            {
                _handle.Dispose();
                _handle = null;
            }
        }

        public void Write(byte[] buf, int len, int timeout)
        {
            if (_handle == null)
            {
                throw new Exception("Device is closed");
            }

            byte[] tmpbuf = new byte[1040];
            tmpbuf[0] = 0;
            Array.Copy(buf, 0, tmpbuf, 1, len);
            var waitHandle = _handle.Stream.BeginWrite(tmpbuf, 0, len + 1, null, null);
            if (!waitHandle.AsyncWaitHandle.WaitOne(timeout))
            {
                throw new Exception("Could not write to device");
            }
        }
    }
}
