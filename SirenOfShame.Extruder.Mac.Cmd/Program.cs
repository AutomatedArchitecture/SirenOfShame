using System;
using HidSharp;
using System.Linq;

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
				for (var i = 0; i < device.MaxInputReportLength; i++) {
					bytes [i] = 0xff;
				}
				bytes [0] = USB_REPORTID_OUT_CONTROL; // ReportId
				bytes [1] = 0; // controlByte1
				bytes [2] = 0; // audio mode
				bytes [3] = 0; // led mode
				bytes [4] = 0xff;	// audio play duration 1
				bytes [5] = 0xff;	// audio play duration 2
					// led play duration 1
					// led play duration 2
					// readAudioIndex
					// readLedIndex

				try {
					stream.Write (bytes);
				}
				catch (TimeoutException)
				{
					Console.WriteLine("Read timed out.");
				}

			}
		}
	}
}
