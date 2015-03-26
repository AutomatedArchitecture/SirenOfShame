using LibUsbDotNet.Main;
using LibUsbDotNet;
using System;

namespace SirenOfShame.Extruder.Mac
{
	public class SirenOfShameDevice : IDisposable
	{
		private UsbDevice _myUsbDevice;
		ILog _log = MyLogManager.GetLogger (typeof(SirenOfShameDevice));

		public SirenOfShameDevice ()
		{
		}

		const int productId = 0x0646;
		const int vendorId = 0x16D0;

		public bool TryConnect() {
			UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder (vendorId, productId);
			_myUsbDevice = UsbDevice.OpenUsbDevice (MyUsbFinder);
			if (_myUsbDevice == null)
				return false;

			_log.Debug ("Device connected");

			return true;
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			// Free usb resources.
			// This is necessary for libusb-1.0 and Linux compatibility.
			UsbDevice.Exit();
		}

		#endregion
	}
}

