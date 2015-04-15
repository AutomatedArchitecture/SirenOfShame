using LibUsbDotNet.Main;
using LibUsbDotNet;
using System;
using System.Runtime.InteropServices;
using MonoLibUsb.Transfer;
using Usb = MonoLibUsb.MonoUsbApi;
using MonoLibUsb;
using MonoLibUsb.Profile;
using System.Collections.Generic;
using MonoLibUsb.Descriptors;

namespace SirenOfShame.Extruder.Mac
{
	public class SirenOfShameDevice : IDisposable
	{
		private UsbDevice _myUsbDevice;
		ILog _log = MyLogManager.GetLogger (typeof(SirenOfShameDevice));
		private const byte LED_MODE_MANUAL = 1;

		public SirenOfShameDevice ()
		{
		}

		const short productId = 0x0646;
		const short vendorId = 0x16D0;

		private const int MY_INTERFACE = 0;
		private const int MY_CONFIG = 1;
		private const int TEST_LOOP_COUNT = 1;
		private const byte MY_EP_WRITE = 0x01;
		private const int MY_TIMEOUT = 2000;
		public const int ReportId_Out_ControlPacket = 1;

		public const int ReportId_In_Info = 1;
		private const int PacketSize = 37 + 1; // (originally report id + packet length PacketSize = 1 + 37;

		public int FirmwareVersion { get; private set; }
		public int HardwareVersion { get; private set; }

		public bool TryConnect() {
			return true;
//			UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder (vendorId, productId);
//			_myUsbDevice = UsbDevice.OpenUsbDevice (MyUsbFinder);
//			return _myUsbDevice != null;
//			return false;
		}

		private static void fillTestData(byte[] data, int len)
		{
			int i;
			for (i = 0; i < len; i++)
				data[i] = (byte) (65 + (i & 0xf));
		}

		private static void memset(byte[] data, int value, int len)
		{
			int i;
			for (i = 0; i < len; i++)
				data[i] = (byte) (value);
		}

		private static MonoUsbSessionHandle sessionHandle  = null;

		// Predicate functions for finding only devices with the specified VendorID & ProductID.
		private static bool MyVidPidPredicate(MonoUsbProfile profile)
		{
			if (profile.DeviceDescriptor.VendorID == vendorId && profile.DeviceDescriptor.ProductID == productId)
				return true;
			return false;
		}

		private void ListAllDevices() {
			// Initialize the context.
			sessionHandle = new MonoUsbSessionHandle();
			if (sessionHandle.IsInvalid)
				throw new Exception(String.Format("Failed intialized libusb context.\n{0}:{1}",
					MonoUsbSessionHandle.LastErrorCode,
					MonoUsbSessionHandle.LastErrorString));

			MonoUsbProfileList profileList = new MonoUsbProfileList();

			// The list is initially empty.
			// Each time refresh is called the list contents are updated. 
			int ret = profileList.Refresh(sessionHandle);
			if (ret < 0) throw new Exception("Failed to retrieve device list.");
			Console.WriteLine("{0} device(s) found.", ret);

			// Use the GetList() method to get a generic List of MonoUsbProfiles
			// Find all profiles that match in the MyVidPidPredicate.
			List<MonoUsbProfile> myVidPidList = profileList.GetList().FindAll(MyVidPidPredicate);

			// myVidPidList reresents a list of connected USB devices that matched
			// in MyVidPidPredicate.
			foreach (MonoUsbProfile profile in myVidPidList)
			{
				// Write the VendorID and ProductID to console output.
				Console.WriteLine("[Device] Vid:{0:X4} Pid:{1:X4}", profile.DeviceDescriptor.VendorID, profile.DeviceDescriptor.ProductID);

				// Loop through all of the devices configurations.
				for (byte i = 0; i < profile.DeviceDescriptor.ConfigurationCount; i++)
				{
					// Get a handle to the configuration.
					MonoUsbConfigHandle configHandle;
					if (MonoUsbApi.GetConfigDescriptor(profile.ProfileHandle, i, out configHandle) < 0) continue;
					if (configHandle.IsInvalid) continue;

					// Create a MonoUsbConfigDescriptor instance for this config handle.
					MonoUsbConfigDescriptor configDescriptor = new MonoUsbConfigDescriptor(configHandle);

					// Write the bConfigurationValue to console output.
					Console.WriteLine("  [Config] bConfigurationValue:{0}", configDescriptor.bConfigurationValue);

					// Interate through the InterfaceList
					foreach (MonoUsbInterface usbInterface in configDescriptor.InterfaceList)
					{
						// Interate through the AltInterfaceList
						foreach (MonoUsbAltInterfaceDescriptor usbAltInterface in usbInterface.AltInterfaceList)
						{
							// Write the bInterfaceNumber and bAlternateSetting to console output.
							Console.WriteLine("    [Interface] bInterfaceNumber:{0} bAlternateSetting:{1}",
								usbAltInterface.bInterfaceNumber,
								usbAltInterface.bAlternateSetting);

							// Interate through the EndpointList
							foreach (MonoUsbEndpointDescriptor endpoint in usbAltInterface.EndpointList)
							{
								// Write the bEndpointAddress, EndpointType, and wMaxPacketSize to console output.
								Console.WriteLine("      [Endpoint] bEndpointAddress:{0:X2} EndpointType:{1} wMaxPacketSize:{2}",
									endpoint.bEndpointAddress,
									(EndpointType) (endpoint.bmAttributes & 0x3),
									endpoint.wMaxPacketSize);
							}
						}
					}
					// Not neccessary, but good programming practice.
					configHandle.Close();
				}
			}
			// Not neccessary, but good programming practice.
			profileList.Close();
			// Not neccessary, but good programming practice.
			sessionHandle.Close();
		}

		public string ReadDeviceInfo()
		{
			bool manualControlSiren = false;
			byte manualSiren = (byte)(manualControlSiren ? 1 : 0);
			return SendControlPacket(
				ledMode: LED_MODE_MANUAL,
				audioMode: manualSiren,
				manualLeds0: 254,
				manualLeds1: 0,
				manualLeds2: 254,
				manualLeds3: 0,
				manualLeds4: (byte)254);

			//DoItTheHardWay ();
			//MoveWith_LibMonoUsb();

			//ListAllDevices ();
			//return null;

//			UsbInfoPacket infoPacket = _deviceInterfaceFile.GetInputReport<UsbInfoPacket>(ReportId_In_Info, PacketSize);
//			_log.Debug("Info packet receieved:");
//			_log.Debug("\tVersion: " + infoPacket.FirmwareVersion);
//			_log.Debug("\tHardwareType: " + infoPacket.HardwareType);
//			_log.Debug("\tHardwareVersion: " + infoPacket.HardwareVersion);
//			_log.Debug("\tExternalMemorySize: " + infoPacket.ExternalMemorySize);
//			_log.Debug("\tAudioMode: " + infoPacket.AudioMode);
//			_log.Debug("\tAudioPlayDuration: " + infoPacket.AudioPlayDuration);
//			_log.Debug("\tLedMode: " + infoPacket.LedMode);
//			_log.Debug("\tLedPlayDuration: " + infoPacket.LedPlayDuration);
//			FirmwareVersion = infoPacket.FirmwareVersion;
//			HardwareType = infoPacket.HardwareType;
//			HardwareVersion = infoPacket.HardwareVersion;
//			return new SirenOfShameInfo(infoPacket);
		}

		private UsbDevice SetupTheEasyWay() {
			UsbDevice robotArm = null;
			// Find and open the usb device.
			var finder = new UsbDeviceFinder(vendorId, productId);
			robotArm = UsbDevice.OpenUsbDevice(finder);

			// If the device is open and ready
			if (robotArm == null)
			{
				Console.WriteLine("Device Not Found.");
				return null;
			}

			// If this is a "whole" usb device (libusb-win32, linux libusb)
			// it will have an IUsbDevice interface. If not (WinUSB) the 
			// variable will be null indicating this is an interface of a 
			// device.
			IUsbDevice wholeUsbDevice = robotArm as IUsbDevice;
			if (!ReferenceEquals(wholeUsbDevice, null))
			{
				Console.WriteLine("SetConfiguration && ClaimInterface");
				// This is a "whole" USB device. Before it can be used, 
				// the desired configuration and interface must be selected.

				// Select config #1
				var setConfigSuccess = wholeUsbDevice.SetConfiguration(MY_CONFIG);

				if (!setConfigSuccess)
					throw new Exception (UsbDevice.LastErrorString);

				// Claim interface #0.
				setConfigSuccess = wholeUsbDevice.ClaimInterface(MY_INTERFACE);

				if (!setConfigSuccess)
					throw new Exception (UsbDevice.LastErrorString);

				}
			return robotArm;
		}

		private string SetOutputReport(UsbControlPacket usbControlPacket, int packetSize) {
			UsbDevice robotArm = null;
			ErrorCode ec = ErrorCode.None;
			try {
				_log.Debug ("Doing it the easy way");
				robotArm = SetupTheEasyWay ();

				SetOutputReport(robotArm, usbControlPacket, packetSize);
				return "";
			}
			catch (Exception ex)
			{
				Console.WriteLine();
				var msg = (ec != ErrorCode.None ? ec + ":" : String.Empty) + "ERRORYO: " + ex.Message;
				Console.WriteLine(msg);
				return msg;
			}
			finally
			{
				if (robotArm != null) 
				{
					if (robotArm.IsOpen)
					{
						// If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
						// it exposes an IUsbDevice interface. If not (WinUSB) the 
						// 'wholeUsbDevice' variable will be null indicating this is 
						// an interface of a device; it does not require or support 
						// configuration and interface selection.
						IUsbDevice wholeUsbDevice = robotArm as IUsbDevice;
						if (!ReferenceEquals(wholeUsbDevice, null))
						{
							// Release interface #0.
							wholeUsbDevice.ReleaseInterface(MY_INTERFACE);
						}

						robotArm.Close();
					}
					// Free usb resources
					UsbDevice.Exit();
				}
			}
		}

		private string SendControlPacket(
			ControlByte1Flags controlByte = ControlByte1Flags.Ignore,
			byte audioMode = (byte)0xff, UInt16 audioDuration = (UInt16) 0xffff,
			byte ledMode = (byte)0xff, UInt16 ledDuration = (UInt16)0xffff,
			byte readAudioIndex = (byte)0xff,
			byte readLedIndex = (byte)0xff,
			byte manualLeds0 = (byte)0xff,
			byte manualLeds1 = (byte)0xff,
			byte manualLeds2 = (byte)0xff,
			byte manualLeds3 = (byte)0xff,
			byte manualLeds4 = (byte)0xff)
		{
			UsbControlPacket usbControlPacket = new UsbControlPacket
			{
				ReportId = ReportId_Out_ControlPacket,
				ControlByte1 = controlByte,
				AudioMode = audioMode,
				AudioDuration = audioDuration,
				LedMode = ledMode,
				LedDuration = ledDuration,
				ReadAudioIndex = readAudioIndex,
				ReadLedIndex = readLedIndex,
				ManualLeds0 = manualLeds0,
				ManualLeds1 = manualLeds1,
				ManualLeds2 = manualLeds2,
				ManualLeds3 = manualLeds3,
				ManualLeds4 = manualLeds4
			};
			return SetOutputReport(usbControlPacket, PacketSize);
		}

		private byte[] ConvertObjToByteArray(object usbControlPacket, int packetSize) {
			byte[] data = new byte[packetSize];
			int objSize = Marshal.SizeOf(usbControlPacket);
			IntPtr dat = Marshal.AllocHGlobal(objSize);
			try
			{
				Marshal.StructureToPtr(usbControlPacket, dat, false);
				Marshal.Copy(dat, data, 0, objSize);
			}
			finally
			{
				Marshal.FreeHGlobal(dat);
			}
			return data;
		}

		private void SetOutputReport(UsbDevice MyUsbDevice, UsbControlPacket usbControlPacket, int packetSize) {
			ErrorCode ec = ErrorCode.None;

			byte[] data = ConvertObjToByteArray (usbControlPacket, packetSize);

			// open read endpoint 1.
			UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

			// open write endpoint 1.
			UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);

			int bytesWritten;
			ec = writer.Write(data, 2000, out bytesWritten);
			if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

			byte[] readBuffer = new byte[1024];
			while (ec == ErrorCode.None)
			{
				int bytesRead;

				// If the device hasn't sent data in the last 100 milliseconds,
				// a timeout error (ec = IoTimedOut) will occur. 
				ec = reader.Read(readBuffer, 100, out bytesRead);

				if (bytesRead == 0) throw new Exception("No more bytes!");

				// Write that output to the console.
				Console.Write(System.Text.Encoding.Default.GetString(readBuffer, 0, bytesRead));
			}

			Console.WriteLine("\r\nDone!\r\n");
		}

		private void SetOutputReportViaControlTransfer(UsbDevice robotArm, UsbControlPacket usbControlPacket, int packetSize) {
			byte[] data = ConvertObjToByteArray (usbControlPacket, packetSize);

			IntPtr dat = Marshal.AllocHGlobal(data.Length);
			Marshal.Copy(data,0,dat,data.Length);

			UsbSetupPacket usbPacket;
			int transferred;

			byte requestType = (byte)(UsbCtrlFlags.Direction_In | UsbCtrlFlags.Recipient_Device); //(byte)(UsbCtrlFlags.Direction_In | UsbCtrlFlags.RequestType_Class | UsbCtrlFlags.Recipient_Interface);
			byte request = (byte)ControlByte1Flags.Ignore;
			short value = 0;
			short index = 0;
			short length = (short)data.Length;

			_log.Debug("Data: " + dat);
			_log.Debug("Dat: " + dat);
			usbPacket = new UsbSetupPacket(requestType, request, value, index, length);
			robotArm.ControlTransfer(ref usbPacket,dat,data.Length,out transferred);
			_log.Debug("Transferred: " + transferred);
			System.Threading.Thread.Sleep(1000);
			_log.Debug("After 1 second transferred: " + transferred);
			_log.Debug("Data: " + dat);
			_log.Debug("Dat: " + dat);
			_log.Debug("Maybe sent data successfully?");		
		}

		public void Dispose ()
		{
			// Free usb resources.
			// This is necessary for libusb-1.0 and Linux compatibility.
			UsbDevice.Exit();
		}
	}
}

