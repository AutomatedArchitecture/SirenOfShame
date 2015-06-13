using System;
using HidSharp;
using System.Linq;
using System.Runtime.InteropServices;

namespace SirenOfShame.Extruder.Mac.Cmd
{
	class MainClass
	{
		private const int LED_MODE_OFF = 0;
		private const int LED_MODE_MANUAL = 1;
		private const int LED_MODE_INTERNAL_START = 2;

		private const int USB_REPORTID_OUT_CONTROL = 1;

		public static void Main (string[] args)
		{
			HidDeviceLoader loader = new HidDeviceLoader();
			var deviceList = loader.GetDevices().ToArray();
			foreach (HidDevice dev in deviceList)
			{
				Console.WriteLine(dev);
			}
			var device = loader.GetDevices(5840, 1606).FirstOrDefault();
			if (device == null) { Console.WriteLine("Failed to open device."); Environment.Exit(1); }

			Console.Write(@"
Max Lengths:
  Input:   {0}
  Output:  {1}
  Feature: {2}

The operating system name for this device is:
  {3}

"
, device.MaxInputReportLength
				, device.MaxOutputReportLength
				, device.MaxFeatureReportLength
				, device.DevicePath
			);

			HidStream stream;
			if (!device.TryOpen(out stream)) { Console.WriteLine("Failed to open device."); Environment.Exit(2); }

			using (stream) {
				var bytes = new byte[device.MaxInputReportLength];
				UsbControlPacket usbControlPacket = new UsbControlPacket ();
				usbControlPacket.ReportId = USB_REPORTID_OUT_CONTROL;
				usbControlPacket.ControlByte1 = 0;
				usbControlPacket.AudioMode = 0;
				usbControlPacket.AudioDuration = 0;
				usbControlPacket.LedMode = LED_MODE_INTERNAL_START;
				usbControlPacket.LedDuration = 0xffff;
				usbControlPacket.ReadAudioIndex = 0xff;
				usbControlPacket.ReadLedIndex = 0xff;
				usbControlPacket.ManualLeds0 = 0xff;
				usbControlPacket.ManualLeds1 = 0xff;
				usbControlPacket.ManualLeds2 = 0xff;
				usbControlPacket.ManualLeds3 = 0xff;
				usbControlPacket.ManualLeds4 = 0xff;

				bytes = getBytes (usbControlPacket);

				try {
					stream.Write (bytes);
				}
				catch (TimeoutException)
				{
					Console.WriteLine("Read timed out.");
				}

			}
		}

		private static byte[] getBytes(UsbControlPacket str) {
			int size = Marshal.SizeOf(str);
			byte[] arr = new byte[size];
			IntPtr ptr = Marshal.AllocHGlobal(size);

			Marshal.StructureToPtr(str, ptr, true);
			Marshal.Copy(ptr, arr, 0, size);
			Marshal.FreeHGlobal(ptr);

			return arr;
		}
	}
}
