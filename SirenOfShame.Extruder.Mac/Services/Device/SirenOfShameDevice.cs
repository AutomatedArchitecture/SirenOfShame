using LibUsbDotNet.Main;
using LibUsbDotNet;
using System;

namespace SirenOfShame.Extruder.Mac
{
	public class SirenOfShameDevice : IDisposable
	{
		private UsbDevice _myUsbDevice;

		public SirenOfShameDevice ()
		{
		}

		public bool TryConnect() {
			UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x16D0, 0x0646);
			_myUsbDevice = UsbDevice.OpenUsbDevice (MyUsbFinder);
			return _myUsbDevice == null;
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

