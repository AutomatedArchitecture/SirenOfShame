using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using HidSharp;

namespace SirenOfShame.Cmd
{
    public class LedPattern
    {
        public int Id { get; set; }
    }

    class MainClass
    {
        private const int LED_MODE_OFF = 0;
        private const int LED_MODE_MANUAL = 1;
        private const int LED_MODE_INTERNAL_START = 2;

        public const byte ReportId_Out_ControlPacket = 1;
        public const byte ReportId_Out_Upload = 2;
        public const byte USB_REPORTID_OUT_LED_CONTROL = 3;
        public const byte USB_REPORTID_INOUT = 5;
        public const byte ReportId_In_Info = 1;
        public const byte ReportId_In_ReadAudioPacket = 3;
        public const byte ReportId_In_ReadLedPacket = 4;

        private const UInt16 Duration_Forever = 0xfffe;
        private const int PacketSize = 1 + 37; // report id + packet length

        private const int USB_REPORTID_OUT_CONTROL = 1;

        public static void Main(string[] args)
        {
            HidDeviceLoader loader = new HidDeviceLoader();
            //var deviceList = loader.GetDevices().ToArray();
            //foreach (HidDevice dev in deviceList)
            //{
            //    Console.WriteLine(dev);
            //}
            var device = loader.GetDevices(5840, 1606).FirstOrDefault();
            if (device == null)
            {
                Console.WriteLine("Failed to open device.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            HidStream stream;
            if (!device.TryOpen(out stream))
            {
                Console.WriteLine("Failed to open device.");
                Environment.Exit(2);
            }

            Console.WriteLine("Connected to: " + device);

            using (stream)
            {
                PlayLightPattern (stream, new LedPattern { Id = 2 }, new TimeSpan (0, 0, 10));

                //var bytes = new byte[PacketSize];
                //bytes[0] = ReportId_In_Info;
                //stream.Read(bytes, 0, PacketSize);
            }

            Console.ReadKey();
        }

        private static void PlayLightPattern(HidStream stream, LedPattern lightPattern, TimeSpan? durationTimeSpan)
        {
            if (lightPattern == null)
            {
                SendControlPacket(stream, ledMode: 0, ledDuration: 0);
            }
            else
            {
                UInt16 duration = CalculateDurationFromTimeSpan(durationTimeSpan);
                SendControlPacket(stream, ledMode: (byte)lightPattern.Id, ledDuration: duration);
            }
        }

        private static void SendControlPacket(HidStream stream,
                                              byte reportId = ReportId_Out_ControlPacket,
                                              ControlByte1Flags controlByte = ControlByte1Flags.Ignore,
                                              byte audioMode = (byte)0xff, UInt16 audioDuration = (UInt16)0xffff,
                                              byte ledMode = (byte)0xff, UInt16 ledDuration = (UInt16)0xffff,
                                              byte readAudioIndex = (byte)0xff,
                                              byte readLedIndex = (byte)0xff,
                                              byte manualLeds0 = (byte)0xff,
                                              byte manualLeds1 = (byte)0xff,
                                              byte manualLeds2 = (byte)0xff,
                                              byte manualLeds3 = (byte)0xff,
                                              byte manualLeds4 = (byte)0xff)
        {
            var usbControlPacket = new UsbControlPacket
            {
                ReportId = reportId,
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

            try
            {
                var bytes = getBytes(usbControlPacket);
                WriteBytes("Sending: ", bytes);
                stream.Write(bytes);
                WriteBytes("Received: ", bytes);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Read timed out.");
            }
        }

        private static void WriteBytes(string msg, byte[] bytes)
        {
            Console.Write(msg);
            for (int i = 0; i < 14; i++)
            {
                Console.Write("{0:X}", bytes[i]);
            }
            Console.WriteLine("");
        }

        private static UInt16 CalculateDurationFromTimeSpan(TimeSpan? durationTimeSpan)
        {
            if (durationTimeSpan == null)
            {
                return Duration_Forever;
            }
            UInt32 result = (UInt32)(durationTimeSpan.Value.TotalSeconds * 10.0);
            if (result > UInt16.MaxValue - 10)
            {
                return Duration_Forever;
            }
            return (UInt16)result;
        }

        private static byte[] getBytes(UsbControlPacket str)
        {
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
